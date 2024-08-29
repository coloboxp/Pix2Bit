using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Pix2Bit
{
    public partial class Form1 : Form
    {
        private bool ignoreEvents = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the combo boxes with values (multiples of 4: 4, 8, 12, ..., 64)
            for (int i = 4; i <= 64; i += 4)
            {
                comboHorizontalBits.Items.Add(i);
                comboVerticalBits.Items.Add(i);
            }

            // Set default selected values for combo boxes
            comboHorizontalBits.SelectedIndex = 0;
            comboVerticalBits.SelectedIndex = 0;

            // Set default selected value for conversion method
            comboConversionMethod.SelectedIndex = 0; // Default to RAW conversion
            comboConversionMethod.SelectedIndexChanged += ComboConversionMethod_SelectedIndexChanged;
        }

        private void ComboConversionMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboConversionMethod.SelectedItem.ToString() == "ks0262 conversion")
            {
                // Automatically set to 16x16 if no table exists
                comboHorizontalBits.SelectedItem = 16;
                comboVerticalBits.SelectedItem = 16;
                BtnGenerateTable_Click(sender, e);
/*                if (tableLayoutPanel1.Controls.Count == 0)
                {
                    // Automatically set to 16x16 if no table exists
                    comboHorizontalBits.SelectedItem = 16;
                    comboVerticalBits.SelectedItem = 16;
                    BtnGenerateTable_Click(sender, e);
                }
                else
                {
                    // Clear existing table
                    ClearAllCheckboxes();
                }*/
            }
        }

        private void BtnGenerateTable_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.Cursor = Cursors.WaitCursor;
            int horizontalBits = (int)comboHorizontalBits.SelectedItem;
            int verticalBits = (int)comboVerticalBits.SelectedItem;

            // Clear existing controls
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            // Set the new column and row count based on the selected values, plus one for labels
            tableLayoutPanel1.ColumnCount = horizontalBits + 1; // Extra column for labels
            tableLayoutPanel1.RowCount = verticalBits + 1;      // Extra row for labels

            // Add column styles dynamically
            for (int i = 0; i <= horizontalBits; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / (horizontalBits + 1)));
            }

            // Add row styles dynamically
            for (int j = 0; j <= verticalBits; j++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / (verticalBits + 1)));
            }

            // Add labels to the first row (columns 0 to 15)
            for (int i = 0; i <= horizontalBits; i++)
            {
                Label label = new Label
                {
                    Text = i.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Consolas", 6),
                    BackColor = Color.LightGray,
                    Tag = i // Set tag to column index
                };
                if (i % 8 == 0 && i != 0)
                {
                    label.Font = new Font(label.Font, FontStyle.Bold);
                }
                label.Click += Label_Click; // Attach click event to show context menu
                label.ContextMenuStrip = CreateColumnContextMenu(i);
                tableLayoutPanel1.Controls.Add(label, i, 0); // Add label to the first row
            }

            // Add labels to the first column (rows 1 to 15)
            for (int j = 1; j <= verticalBits; j++)
            {
                Label label = new Label
                {
                    Text = j.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Consolas", 6),
                    BackColor = Color.LightGray,
                    Tag = j // Set tag to row index
                };
                if (j % 8 == 0 && j != 0)
                {
                    label.Font = new Font(label.Font, FontStyle.Bold);
                }
                label.Click += Label_Click; // Attach click event to show context menu
                label.ContextMenuStrip = CreateRowContextMenu(j);
                tableLayoutPanel1.Controls.Add(label, 0, j); // Add label to the first column
            }

            // Initialize a ToolTip to use for checkboxes
            ToolTip toolTip = new ToolTip();

            // Dynamically add checkboxes to the table layout starting from row 1 and column 1
            for (int i = 1; i <= verticalBits; i++)
            {
                for (int j = 1; j <= horizontalBits; j++)
                {
                    CheckBox led = new CheckBox
                    {
                        Appearance = Appearance.Button,
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        Font = new Font("Courier New", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                        BackColor = Color.Gray, // Default color
                        FlatStyle = FlatStyle.Flat // Remove border bleed effect
                    };
                    led.Click += new EventHandler(this.checkBox_Click);

                    // Set tooltip with numeric coordinates
                    toolTip.SetToolTip(led, $"Row {i}, Col {j}");

                    // Add the checkbox to the table at the specified location
                    tableLayoutPanel1.Controls.Add(led, j, i);
                }
            }

            // Update the hexadecimal value on generation
            UpdateHexValue();
            this.ResumeLayout();
            this.Cursor = Cursors.Default;
        }

        private ContextMenuStrip CreateRowContextMenu(int rowIndex)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Low all", null, (s, e) => SetRowCheckboxes(false, rowIndex));
            contextMenu.Items.Add("High all", null, (s, e) => SetRowCheckboxes(true, rowIndex));
            contextMenu.Items.Add("Invert all", null, (s, e) => InvertRowCheckboxes(rowIndex));
            return contextMenu;
        }

        private ContextMenuStrip CreateColumnContextMenu(int colIndex)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Low all", null, (s, e) => SetColumnCheckboxes(false, colIndex));
            contextMenu.Items.Add("High all", null, (s, e) => SetColumnCheckboxes(true, colIndex));
            contextMenu.Items.Add("Invert all", null, (s, e) => InvertColumnCheckboxes(colIndex));
            return contextMenu;
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null && label.ContextMenuStrip != null)
            {
                // Show context menu at the cursor's position
                label.ContextMenuStrip.Show(Cursor.Position);
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                UpdateHexValue();
            }
        }

        private void BtnHighAll_Click(object sender, EventArgs e)
        {
            SetAllCheckboxes(true);
        }

        private void BtnLowAll_Click(object sender, EventArgs e)
        {
            SetAllCheckboxes(false);
        }

        private void BtnInvertAll_Click(object sender, EventArgs e)
        {
            InvertAllCheckboxes();
        }

        private void SetAllCheckboxes(bool state)
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = state;
                    checkBox.BackColor = state ? Color.Red : Color.Gray;
                }
            }

            UpdateHexValue();
        }

        private void InvertAllCheckboxes()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = !checkBox.Checked;
                    checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                }
            }

            UpdateHexValue();
        }

        private void SetRowCheckboxes(bool state, int rowIndex)
        {
            if (rowIndex > 0)
            {
                for (int col = 1; col < tableLayoutPanel1.ColumnCount; col++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(col, rowIndex) is CheckBox checkBox)
                    {
                        checkBox.Checked = state;
                        checkBox.BackColor = state ? Color.Red : Color.Gray;
                    }
                }
                UpdateHexValue();
            }
        }

        private void InvertRowCheckboxes(int rowIndex)
        {
            if (rowIndex > 0)
            {
                for (int col = 1; col < tableLayoutPanel1.ColumnCount; col++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(col, rowIndex) is CheckBox checkBox)
                    {
                        checkBox.Checked = !checkBox.Checked;
                        checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                    }
                }
                UpdateHexValue();
            }
        }

        private void SetColumnCheckboxes(bool state, int colIndex)
        {
            if (colIndex > 0)
            {
                for (int row = 1; row < tableLayoutPanel1.RowCount; row++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(colIndex, row) is CheckBox checkBox)
                    {
                        checkBox.Checked = state;
                        checkBox.BackColor = state ? Color.Red : Color.Gray;
                    }
                }
                UpdateHexValue();
            }
        }

        private void InvertColumnCheckboxes(int colIndex)
        {
            if (colIndex > 0)
            {
                for (int row = 1; row < tableLayoutPanel1.RowCount; row++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(colIndex, row) is CheckBox checkBox)
                    {
                        checkBox.Checked = !checkBox.Checked;
                        checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                    }
                }
                UpdateHexValue();
            }
        }

        private string[] CleanHexInput(string input)
        {
            // Remove unexpected characters, allowing only valid hex digits and commas
            string cleanedInput = Regex.Replace(input, @"[^0-9A-Fa-f,]", "");

            // Split the cleaned input into an array of hex values
            string[] hexValues = cleanedInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Ensure each hex value is correctly formatted with '0x'
            for (int i = 0; i < hexValues.Length; i++)
            {
                hexValues[i] = hexValues[i].TrimStart('0'); // Remove leading zeros
                if (hexValues[i].Length == 0)
                {
                    hexValues[i] = "0"; // Ensure that "0" remains "0"
                }
                hexValues[i] = "0x" + hexValues[i].PadLeft(2, '0'); // Add '0x' and ensure two hex digits
            }

            return hexValues;
        }

        private void ClearAllCheckboxes()
        {
            ignoreEvents = true;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                    checkBox.BackColor = Color.Gray;
                }
            }
            ignoreEvents = false;
        }

        private void UpdateHexValue()
        {
            if (!ignoreEvents)
            {
                txtHexValue.Text = GetHexSequence();
            }
        }

        private string GetHexSequence()
        {
            if (comboConversionMethod.SelectedItem.ToString() == "ks0262 conversion")
            {
                return GetHexSequenceKS0262();
            }
            else // RAW conversion
            {
                return GetHexSequenceRAW();
            }
        }

        private string GetHexSequenceRAW()
        {
            string hexSequence = "";
            int cols = tableLayoutPanel1.ColumnCount - 1; // Exclude label column
            int rows = tableLayoutPanel1.RowCount - 1;    // Exclude label row

            for (int i = 1; i <= rows; i++)
            {
                string binaryString = "";

                for (int j = 1; j <= cols; j++)
                {
                    // Access the checkbox control in each cell
                    if (tableLayoutPanel1.GetControlFromPosition(j, i) is CheckBox checkBox)
                    {
                        binaryString += checkBox.Checked ? "1" : "0";
                    }
                }

                // Pad the binary string to make its length a multiple of 8
                while (binaryString.Length % 8 != 0)
                {
                    binaryString = "0" + binaryString;
                }

                // Convert binary string to hexadecimal
                for (int k = 0; k < binaryString.Length; k += 8)
                {
                    string byteString = binaryString.Substring(k, 8);
                    int byteValue = Convert.ToInt32(byteString, 2);
                    hexSequence += $"0x{byteValue:X2}, "; // Ensure two-digit hex
                }
            }

            // Remove trailing comma and space
            if (hexSequence.EndsWith(", "))
            {
                hexSequence = hexSequence.Remove(hexSequence.Length - 2);
            }

            return hexSequence;
        }

        private string GetHexSequenceKS0262()
        {
            string hexSequence = "";
            int cols = tableLayoutPanel1.ColumnCount - 1; // Exclude label column
            int rows = tableLayoutPanel1.RowCount - 1;    // Exclude label row

            // Create two separate lists for each byte group
            List<byte> firstGroup = new List<byte>();
            List<byte> secondGroup = new List<byte>();

            // Iterate over each row to extract bytes, starting from row 1 (skip labels)
            for (int row = 1; row <= rows; row++)
            {
                string binaryString1 = "";
                string binaryString2 = "";

                // First byte (first 8 columns), starting from column 1 (skip labels)
                for (int col = 1; col <= Math.Min(8, cols); col++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(col, row) is CheckBox checkBox)
                    {
                        binaryString1 += checkBox.Checked ? "1" : "0";
                    }
                }

                // Second byte (columns 9-16), starting from column 9 (skip labels)
                for (int col = 9; col <= Math.Min(16, cols); col++)
                {
                    if (tableLayoutPanel1.GetControlFromPosition(col, row) is CheckBox checkBox)
                    {
                        binaryString2 += checkBox.Checked ? "1" : "0";
                    }
                }

                // Convert binary strings to byte values
                byte byteValue1 = Convert.ToByte(binaryString1.PadLeft(8, '0'), 2);
                byte byteValue2 = Convert.ToByte(binaryString2.PadLeft(8, '0'), 2);

                firstGroup.Add(byteValue1);
                secondGroup.Add(byteValue2);
            }

            // Combine the firstGroup and secondGroup to match ks0262 order
            for (int i = 0; i < firstGroup.Count; i++)
            {
                hexSequence += $"0x{firstGroup[i]:X2},"; // Ensure two-digit hex
            }
            for (int i = 0; i < secondGroup.Count; i++)
            {
                hexSequence += $"0x{secondGroup[i]:X2},"; // Ensure two-digit hex
            }

            // Remove trailing comma
            if (hexSequence.EndsWith(","))
            {
                hexSequence = hexSequence.Remove(hexSequence.Length - 1);
            }

            return hexSequence;
        }

        private void BtnByteToMatrix_Click(object sender, EventArgs e)
        {
            // Convert the hex sequence in the txtHexValue TextBox to matrix
            try
            {
                string pastedText = txtHexValue.Text.Trim();
                string[] hexValues = CleanHexInput(pastedText);

                txtHexValue.Text = string.Join(",", hexValues);

                ignoreEvents = true; // Prevent updating hex during matrix conversion
                ClearAllCheckboxes();

                if (comboConversionMethod.SelectedItem.ToString() == "ks0262 conversion")
                {
                    ConvertToMatrixKS0262(hexValues);
                }
                else // RAW conversion
                {
                    ConvertToMatrixRAW(hexValues);
                }

                ignoreEvents = false; // Resume updating hex
                UpdateHexValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing hexadecimal values: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConvertToMatrixRAW(string[] hexValues)
        {
            int cols = tableLayoutPanel1.ColumnCount - 1; // Exclude label column
            int rows = tableLayoutPanel1.RowCount - 1;    // Exclude label row

            int bitIndex = 0;

            for (int i = 1; i <= rows; i++)
            {
                string binaryString = "";

                for (int j = 1; j <= cols / 8; j++)
                {
                    if (bitIndex < hexValues.Length)
                    {
                        byte byteValue = Convert.ToByte(hexValues[bitIndex], 16);
                        binaryString += Convert.ToString(byteValue, 2).PadLeft(8, '0');
                        bitIndex++;
                    }
                }

                for (int j = 1; j <= cols; j++)
                {
                    if (j <= binaryString.Length)
                    {
                        if (tableLayoutPanel1.GetControlFromPosition(j, i) is CheckBox checkBox)
                        {
                            checkBox.Checked = binaryString[j - 1] == '1';
                            checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                        }
                    }
                }
            }
        }

        private void ConvertToMatrixKS0262(string[] hexValues)
        {
            int cols = tableLayoutPanel1.ColumnCount - 1; // Exclude label column
            int rows = tableLayoutPanel1.RowCount - 1;    // Exclude label row

            if (hexValues.Length < 32)
            {
                MessageBox.Show("Not enough bytes for ks0262 conversion. Please provide a valid byte array.");
                return;
            }

            int halfLength = hexValues.Length / 2; // To separate the first and second groups

            for (int row = 1; row <= Math.Min(rows, 16); row++) // Ensure within 16 rows
            {
                if (row <= halfLength) // Ensure we do not go out of bounds
                {
                    byte byte1 = Convert.ToByte(hexValues[row - 1], 16);
                    byte byte2 = Convert.ToByte(hexValues[row + halfLength - 1], 16);

                    string combinedBinaryString = Convert.ToString(byte1, 2).PadLeft(8, '0') + Convert.ToString(byte2, 2).PadLeft(8, '0');

                    for (int col = 1; col <= Math.Min(cols, 16); col++) // Ensure within 16 columns
                    {
                        if (tableLayoutPanel1.GetControlFromPosition(col, row) is CheckBox checkBox)
                        {
                            checkBox.Checked = combinedBinaryString[col - 1] == '1';
                            checkBox.BackColor = checkBox.Checked ? Color.Red : Color.Gray;
                        }
                    }
                }
            }
        }
    }
}
