namespace GPS_Coordinate_Calculator
{
    partial class formCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCalculate = new System.Windows.Forms.Button();
            this.tbLongitudeC1 = new System.Windows.Forms.TextBox();
            this.tbLattitudeC1 = new System.Windows.Forms.TextBox();
            this.lbLattitudeC1 = new System.Windows.Forms.Label();
            this.lbLongitudeC1 = new System.Windows.Forms.Label();
            this.lblCoordinate2 = new System.Windows.Forms.Label();
            this.lblLattitudec2 = new System.Windows.Forms.Label();
            this.lblCoordinate1 = new System.Windows.Forms.Label();
            this.tbLattitudeC2 = new System.Windows.Forms.TextBox();
            this.tbLongitudeC2 = new System.Windows.Forms.TextBox();
            this.lblLongitudeC2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.labelDistance = new System.Windows.Forms.Label();
            this.lblTotalDistance = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbTimeTaken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAvgSpeed = new System.Windows.Forms.Label();
            this.lblDistanceSprinst = new System.Windows.Forms.Label();
            this.lblSprints = new System.Windows.Forms.Label();
            this.lblNrOfSprints = new System.Windows.Forms.Label();
            this.lblAvgSprintDistance = new System.Windows.Forms.Label();
            this.lblAvgSpeedNr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(12, 141);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // tbLongitudeC1
            // 
            this.tbLongitudeC1.Location = new System.Drawing.Point(69, 45);
            this.tbLongitudeC1.Name = "tbLongitudeC1";
            this.tbLongitudeC1.Size = new System.Drawing.Size(204, 20);
            this.tbLongitudeC1.TabIndex = 1;
            // 
            // tbLattitudeC1
            // 
            this.tbLattitudeC1.Location = new System.Drawing.Point(69, 22);
            this.tbLattitudeC1.Name = "tbLattitudeC1";
            this.tbLattitudeC1.Size = new System.Drawing.Size(204, 20);
            this.tbLattitudeC1.TabIndex = 2;
            // 
            // lbLattitudeC1
            // 
            this.lbLattitudeC1.AutoSize = true;
            this.lbLattitudeC1.Location = new System.Drawing.Point(12, 22);
            this.lbLattitudeC1.Name = "lbLattitudeC1";
            this.lbLattitudeC1.Size = new System.Drawing.Size(48, 13);
            this.lbLattitudeC1.TabIndex = 3;
            this.lbLattitudeC1.Text = "Lattitude";
            // 
            // lbLongitudeC1
            // 
            this.lbLongitudeC1.AutoSize = true;
            this.lbLongitudeC1.Location = new System.Drawing.Point(12, 48);
            this.lbLongitudeC1.Name = "lbLongitudeC1";
            this.lbLongitudeC1.Size = new System.Drawing.Size(51, 13);
            this.lbLongitudeC1.TabIndex = 4;
            this.lbLongitudeC1.Text = "Longitute";
            // 
            // lblCoordinate2
            // 
            this.lblCoordinate2.AutoSize = true;
            this.lblCoordinate2.Location = new System.Drawing.Point(12, 71);
            this.lblCoordinate2.Name = "lblCoordinate2";
            this.lblCoordinate2.Size = new System.Drawing.Size(67, 13);
            this.lblCoordinate2.TabIndex = 5;
            this.lblCoordinate2.Text = "Coordinate 2";
            // 
            // lblLattitudec2
            // 
            this.lblLattitudec2.AutoSize = true;
            this.lblLattitudec2.Location = new System.Drawing.Point(12, 87);
            this.lblLattitudec2.Name = "lblLattitudec2";
            this.lblLattitudec2.Size = new System.Drawing.Size(48, 13);
            this.lblLattitudec2.TabIndex = 6;
            this.lblLattitudec2.Text = "Lattitude";
            // 
            // lblCoordinate1
            // 
            this.lblCoordinate1.AutoSize = true;
            this.lblCoordinate1.Location = new System.Drawing.Point(12, 6);
            this.lblCoordinate1.Name = "lblCoordinate1";
            this.lblCoordinate1.Size = new System.Drawing.Size(67, 13);
            this.lblCoordinate1.TabIndex = 7;
            this.lblCoordinate1.Text = "Coordinate 1";
            // 
            // tbLattitudeC2
            // 
            this.tbLattitudeC2.Location = new System.Drawing.Point(69, 87);
            this.tbLattitudeC2.Name = "tbLattitudeC2";
            this.tbLattitudeC2.Size = new System.Drawing.Size(204, 20);
            this.tbLattitudeC2.TabIndex = 9;
            // 
            // tbLongitudeC2
            // 
            this.tbLongitudeC2.Location = new System.Drawing.Point(69, 111);
            this.tbLongitudeC2.Name = "tbLongitudeC2";
            this.tbLongitudeC2.Size = new System.Drawing.Size(204, 20);
            this.tbLongitudeC2.TabIndex = 10;
            // 
            // lblLongitudeC2
            // 
            this.lblLongitudeC2.AutoSize = true;
            this.lblLongitudeC2.Location = new System.Drawing.Point(12, 114);
            this.lblLongitudeC2.Name = "lblLongitudeC2";
            this.lblLongitudeC2.Size = new System.Drawing.Size(54, 13);
            this.lblLongitudeC2.TabIndex = 11;
            this.lblLongitudeC2.Text = "Longitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Distance:";
            // 
            // tbDistance
            // 
            this.tbDistance.Location = new System.Drawing.Point(176, 144);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(97, 20);
            this.tbDistance.TabIndex = 14;
            // 
            // labelDistance
            // 
            this.labelDistance.AutoSize = true;
            this.labelDistance.Location = new System.Drawing.Point(119, 321);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(79, 13);
            this.labelDistance.TabIndex = 16;
            this.labelDistance.Text = "Total Distance:";
            // 
            // lblTotalDistance
            // 
            this.lblTotalDistance.AutoSize = true;
            this.lblTotalDistance.Location = new System.Drawing.Point(194, 321);
            this.lblTotalDistance.Name = "lblTotalDistance";
            this.lblTotalDistance.Size = new System.Drawing.Size(42, 13);
            this.lblTotalDistance.TabIndex = 17;
            this.lblTotalDistance.Text = "0 meter";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(12, 178);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFile.TabIndex = 18;
            this.btnBrowseFile.Text = "Search File";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(12, 207);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(261, 20);
            this.tbFileName.TabIndex = 19;
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(176, 232);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(97, 23);
            this.btnReadFile.TabIndex = 20;
            this.btnReadFile.Text = "Calculate";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(8, 288);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 47);
            this.btnReset.TabIndex = 21;
            this.btnReset.Text = "Reset Stats";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbTimeTaken
            // 
            this.tbTimeTaken.Location = new System.Drawing.Point(91, 235);
            this.tbTimeTaken.Name = "tbTimeTaken";
            this.tbTimeTaken.Size = new System.Drawing.Size(45, 20);
            this.tbTimeTaken.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Time in seconds:";
            // 
            // lblAvgSpeed
            // 
            this.lblAvgSpeed.AutoSize = true;
            this.lblAvgSpeed.Location = new System.Drawing.Point(114, 307);
            this.lblAvgSpeed.Name = "lblAvgSpeed";
            this.lblAvgSpeed.Size = new System.Drawing.Size(87, 13);
            this.lblAvgSpeed.TabIndex = 24;
            this.lblAvgSpeed.Text = "Average Speed: ";
            // 
            // lblDistanceSprinst
            // 
            this.lblDistanceSprinst.AutoSize = true;
            this.lblDistanceSprinst.Location = new System.Drawing.Point(73, 294);
            this.lblDistanceSprinst.Name = "lblDistanceSprinst";
            this.lblDistanceSprinst.Size = new System.Drawing.Size(125, 13);
            this.lblDistanceSprinst.TabIndex = 25;
            this.lblDistanceSprinst.Text = "Average Sprint Distance:";
            // 
            // lblSprints
            // 
            this.lblSprints.AutoSize = true;
            this.lblSprints.Location = new System.Drawing.Point(107, 281);
            this.lblSprints.Name = "lblSprints";
            this.lblSprints.Size = new System.Drawing.Size(91, 13);
            this.lblSprints.TabIndex = 26;
            this.lblSprints.Text = "Amount of sprints:";
            // 
            // lblNrOfSprints
            // 
            this.lblNrOfSprints.AutoSize = true;
            this.lblNrOfSprints.Location = new System.Drawing.Point(194, 281);
            this.lblNrOfSprints.Name = "lblNrOfSprints";
            this.lblNrOfSprints.Size = new System.Drawing.Size(46, 13);
            this.lblNrOfSprints.TabIndex = 27;
            this.lblNrOfSprints.Text = "0 sprints";
            // 
            // lblAvgSprintDistance
            // 
            this.lblAvgSprintDistance.AutoSize = true;
            this.lblAvgSprintDistance.Location = new System.Drawing.Point(194, 294);
            this.lblAvgSprintDistance.Name = "lblAvgSprintDistance";
            this.lblAvgSprintDistance.Size = new System.Drawing.Size(42, 13);
            this.lblAvgSprintDistance.TabIndex = 28;
            this.lblAvgSprintDistance.Text = "0 meter";
            // 
            // lblAvgSpeedNr
            // 
            this.lblAvgSpeedNr.AutoSize = true;
            this.lblAvgSpeedNr.Location = new System.Drawing.Point(194, 307);
            this.lblAvgSpeedNr.Name = "lblAvgSpeedNr";
            this.lblAvgSpeedNr.Size = new System.Drawing.Size(44, 13);
            this.lblAvgSpeedNr.TabIndex = 29;
            this.lblAvgSpeedNr.Text = "0 km/h ";
            // 
            // formCalculator
            // 
            this.AcceptButton = this.btnCalculate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 342);
            this.Controls.Add(this.lblAvgSpeedNr);
            this.Controls.Add(this.lblAvgSprintDistance);
            this.Controls.Add(this.lblNrOfSprints);
            this.Controls.Add(this.lblSprints);
            this.Controls.Add(this.lblDistanceSprinst);
            this.Controls.Add(this.lblAvgSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTimeTaken);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.lblTotalDistance);
            this.Controls.Add(this.labelDistance);
            this.Controls.Add(this.tbDistance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLongitudeC2);
            this.Controls.Add(this.tbLongitudeC2);
            this.Controls.Add(this.tbLattitudeC2);
            this.Controls.Add(this.lblCoordinate1);
            this.Controls.Add(this.lblLattitudec2);
            this.Controls.Add(this.lblCoordinate2);
            this.Controls.Add(this.lbLongitudeC1);
            this.Controls.Add(this.lbLattitudeC1);
            this.Controls.Add(this.tbLattitudeC1);
            this.Controls.Add(this.tbLongitudeC1);
            this.Controls.Add(this.btnCalculate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "formCalculator";
            this.Text = "GPS Coordinate Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox tbLongitudeC1;
        private System.Windows.Forms.TextBox tbLattitudeC1;
        private System.Windows.Forms.Label lbLattitudeC1;
        private System.Windows.Forms.Label lbLongitudeC1;
        private System.Windows.Forms.Label lblCoordinate2;
        private System.Windows.Forms.Label lblLattitudec2;
        private System.Windows.Forms.Label lblCoordinate1;
        private System.Windows.Forms.TextBox tbLattitudeC2;
        private System.Windows.Forms.TextBox tbLongitudeC2;
        private System.Windows.Forms.Label lblLongitudeC2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.Label lblTotalDistance;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox tbTimeTaken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAvgSpeed;
        private System.Windows.Forms.Label lblDistanceSprinst;
        private System.Windows.Forms.Label lblSprints;
        private System.Windows.Forms.Label lblNrOfSprints;
        private System.Windows.Forms.Label lblAvgSprintDistance;
        private System.Windows.Forms.Label lblAvgSpeedNr;
    }
}

