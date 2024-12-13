namespace gk_2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            betaTrackBar = new TrackBar();
            alphaTrackBar = new TrackBar();
            alphaLabel = new Label();
            betaLabel = new Label();
            normalVectorCheckBox = new CheckBox();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            kdTrackBar = new TrackBar();
            kdLabel = new Label();
            colorButton = new Button();
            animationTrackBar = new TrackBar();
            animationLabel = new Label();
            triangulationTrackBar = new TrackBar();
            traingulationLabel = new Label();
            ksTrackBar = new TrackBar();
            mTrackBar = new TrackBar();
            ksLabel = new Label();
            mLabel = new Label();
            animationRadioButton = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            normalMap = new Button();
            waveCheckbox = new CheckBox();
            reflectorCheckBox = new CheckBox();
            mlTrackBar = new TrackBar();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)betaTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alphaTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)animationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)triangulationTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mlTrackBar).BeginInit();
            SuspendLayout();
            // 
            // betaTrackBar
            // 
            betaTrackBar.Location = new Point(12, 107);
            betaTrackBar.Name = "betaTrackBar";
            betaTrackBar.Size = new Size(130, 56);
            betaTrackBar.TabIndex = 0;
            betaTrackBar.Scroll += betaTrackBar_Scroll;
            // 
            // alphaTrackBar
            // 
            alphaTrackBar.Location = new Point(12, 45);
            alphaTrackBar.Maximum = 45;
            alphaTrackBar.Minimum = -45;
            alphaTrackBar.Name = "alphaTrackBar";
            alphaTrackBar.Size = new Size(130, 56);
            alphaTrackBar.TabIndex = 1;
            alphaTrackBar.Tag = "";
            alphaTrackBar.Scroll += alphaTrackBar_Scroll;
            // 
            // alphaLabel
            // 
            alphaLabel.AutoSize = true;
            alphaLabel.Location = new Point(12, 22);
            alphaLabel.Name = "alphaLabel";
            alphaLabel.Size = new Size(34, 20);
            alphaLabel.TabIndex = 2;
            alphaLabel.Text = "alfa";
            // 
            // betaLabel
            // 
            betaLabel.AutoSize = true;
            betaLabel.Location = new Point(12, 84);
            betaLabel.Name = "betaLabel";
            betaLabel.Size = new Size(39, 20);
            betaLabel.TabIndex = 3;
            betaLabel.Text = "beta";
            // 
            // normalVectorCheckBox
            // 
            normalVectorCheckBox.AutoSize = true;
            normalVectorCheckBox.Location = new Point(12, 450);
            normalVectorCheckBox.Name = "normalVectorCheckBox";
            normalVectorCheckBox.Size = new Size(138, 24);
            normalVectorCheckBox.TabIndex = 4;
            normalVectorCheckBox.Text = "Mapa wektorów";
            normalVectorCheckBox.UseVisualStyleBackColor = true;
            normalVectorCheckBox.CheckedChanged += normalVectorCheckBox_CheckedChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(200, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1161, 619);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 480);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(107, 24);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Rysuj siatkę";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // kdTrackBar
            // 
            kdTrackBar.Location = new Point(12, 254);
            kdTrackBar.Name = "kdTrackBar";
            kdTrackBar.Size = new Size(130, 56);
            kdTrackBar.TabIndex = 7;
            kdTrackBar.Scroll += kdTrackBar_Scroll;
            // 
            // kdLabel
            // 
            kdLabel.AutoSize = true;
            kdLabel.Location = new Point(12, 231);
            kdLabel.Name = "kdLabel";
            kdLabel.Size = new Size(25, 20);
            kdLabel.TabIndex = 8;
            kdLabel.Text = "kd";
            // 
            // colorButton
            // 
            colorButton.Location = new Point(12, 510);
            colorButton.Name = "colorButton";
            colorButton.Size = new Size(162, 29);
            colorButton.TabIndex = 9;
            colorButton.Text = "Kolor światła";
            colorButton.UseVisualStyleBackColor = true;
            colorButton.Click += colorButton_Click;
            // 
            // animationTrackBar
            // 
            animationTrackBar.Location = new Point(12, 608);
            animationTrackBar.Maximum = 500;
            animationTrackBar.Name = "animationTrackBar";
            animationTrackBar.Size = new Size(130, 56);
            animationTrackBar.TabIndex = 11;
            animationTrackBar.Scroll += animationTrackBar_Scroll;
            // 
            // animationLabel
            // 
            animationLabel.AutoSize = true;
            animationLabel.Location = new Point(12, 585);
            animationLabel.Name = "animationLabel";
            animationLabel.Size = new Size(108, 20);
            animationLabel.TabIndex = 12;
            animationLabel.Text = "z (do animacji)";
            // 
            // triangulationTrackBar
            // 
            triangulationTrackBar.Location = new Point(12, 172);
            triangulationTrackBar.Maximum = 15;
            triangulationTrackBar.Minimum = 1;
            triangulationTrackBar.Name = "triangulationTrackBar";
            triangulationTrackBar.Size = new Size(130, 56);
            triangulationTrackBar.TabIndex = 13;
            triangulationTrackBar.Value = 5;
            triangulationTrackBar.Scroll += triangulationTrackBar_Scroll;
            // 
            // traingulationLabel
            // 
            traingulationLabel.AutoSize = true;
            traingulationLabel.Location = new Point(12, 143);
            traingulationLabel.Name = "traingulationLabel";
            traingulationLabel.Size = new Size(87, 20);
            traingulationLabel.TabIndex = 14;
            traingulationLabel.Text = "Dokładność";
            // 
            // ksTrackBar
            // 
            ksTrackBar.Location = new Point(12, 316);
            ksTrackBar.Name = "ksTrackBar";
            ksTrackBar.Size = new Size(130, 56);
            ksTrackBar.TabIndex = 15;
            ksTrackBar.Scroll += trackBar1_Scroll;
            // 
            // mTrackBar
            // 
            mTrackBar.Location = new Point(12, 388);
            mTrackBar.Maximum = 100;
            mTrackBar.Name = "mTrackBar";
            mTrackBar.Size = new Size(130, 56);
            mTrackBar.TabIndex = 16;
            mTrackBar.Scroll += mTrackBar_Scroll;
            // 
            // ksLabel
            // 
            ksLabel.AutoSize = true;
            ksLabel.Location = new Point(12, 293);
            ksLabel.Name = "ksLabel";
            ksLabel.Size = new Size(22, 20);
            ksLabel.TabIndex = 17;
            ksLabel.Text = "ks";
            // 
            // mLabel
            // 
            mLabel.AutoSize = true;
            mLabel.Location = new Point(12, 365);
            mLabel.Name = "mLabel";
            mLabel.Size = new Size(22, 20);
            mLabel.TabIndex = 18;
            mLabel.Text = "m";
            // 
            // animationRadioButton
            // 
            animationRadioButton.AutoSize = true;
            animationRadioButton.Location = new Point(12, 558);
            animationRadioButton.Name = "animationRadioButton";
            animationRadioButton.Size = new Size(93, 24);
            animationRadioButton.TabIndex = 19;
            animationRadioButton.Text = "Animacja";
            animationRadioButton.UseVisualStyleBackColor = true;
            animationRadioButton.CheckedChanged += animationRadioButton_CheckedChanged_1;
            // 
            // button1
            // 
            button1.Location = new Point(12, 652);
            button1.Name = "button1";
            button1.Size = new Size(162, 29);
            button1.TabIndex = 20;
            button1.Text = "Tekstura";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(200, 652);
            button2.Name = "button2";
            button2.Size = new Size(162, 29);
            button2.TabIndex = 21;
            button2.Text = "Kolor obiektu";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // normalMap
            // 
            normalMap.Location = new Point(389, 652);
            normalMap.Name = "normalMap";
            normalMap.Size = new Size(162, 29);
            normalMap.TabIndex = 22;
            normalMap.Text = "Wczytaj wektory";
            normalMap.UseVisualStyleBackColor = true;
            normalMap.Click += normalMap_Click;
            // 
            // waveCheckbox
            // 
            waveCheckbox.AutoSize = true;
            waveCheckbox.Location = new Point(592, 657);
            waveCheckbox.Name = "waveCheckbox";
            waveCheckbox.Size = new Size(97, 24);
            waveCheckbox.TabIndex = 23;
            waveCheckbox.Text = "Falowanie";
            waveCheckbox.UseVisualStyleBackColor = true;
            waveCheckbox.CheckedChanged += waveCheckbox_CheckedChanged;
            // 
            // reflectorCheckBox
            // 
            reflectorCheckBox.AutoSize = true;
            reflectorCheckBox.Location = new Point(715, 655);
            reflectorCheckBox.Name = "reflectorCheckBox";
            reflectorCheckBox.Size = new Size(91, 24);
            reflectorCheckBox.TabIndex = 24;
            reflectorCheckBox.Text = "Reflektor";
            reflectorCheckBox.UseVisualStyleBackColor = true;
            reflectorCheckBox.CheckedChanged += reflectorCheckBox_CheckedChanged;
            // 
            // mlTrackBar
            // 
            mlTrackBar.Location = new Point(820, 685);
            mlTrackBar.Name = "mlTrackBar";
            mlTrackBar.Size = new Size(130, 56);
            mlTrackBar.TabIndex = 25;
            mlTrackBar.Scroll += mlTrackBar_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(838, 652);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 26;
            label1.Text = "ml (reflektor)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1428, 753);
            Controls.Add(label1);
            Controls.Add(mlTrackBar);
            Controls.Add(reflectorCheckBox);
            Controls.Add(waveCheckbox);
            Controls.Add(normalMap);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(animationRadioButton);
            Controls.Add(mLabel);
            Controls.Add(ksLabel);
            Controls.Add(mTrackBar);
            Controls.Add(ksTrackBar);
            Controls.Add(traingulationLabel);
            Controls.Add(triangulationTrackBar);
            Controls.Add(animationLabel);
            Controls.Add(animationTrackBar);
            Controls.Add(colorButton);
            Controls.Add(kdLabel);
            Controls.Add(kdTrackBar);
            Controls.Add(checkBox1);
            Controls.Add(pictureBox1);
            Controls.Add(normalVectorCheckBox);
            Controls.Add(betaLabel);
            Controls.Add(alphaLabel);
            Controls.Add(alphaTrackBar);
            Controls.Add(betaTrackBar);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)betaTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)alphaTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)animationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)triangulationTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ksTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)mTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)mlTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar betaTrackBar;
        private TrackBar alphaTrackBar;
        private Label alphaLabel;
        private Label betaLabel;
        private CheckBox normalVectorCheckBox;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private TrackBar kdTrackBar;
        private Label kdLabel;
        private Button colorButton;
        private TrackBar animationTrackBar;
        private Label animationLabel;
        private TrackBar triangulationTrackBar;
        private Label traingulationLabel;
        private TrackBar ksTrackBar;
        private TrackBar mTrackBar;
        private Label ksLabel;
        private Label mLabel;
        private CheckBox animationRadioButton;
        private Button button1;
        private Button button2;
        private Button normalMap;
        private CheckBox waveCheckbox;
        private CheckBox reflectorCheckBox;
        private TrackBar mlTrackBar;
        private Label label1;
    }
}
