namespace CyberKnightGUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnAsk = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.lblResponse = new System.Windows.Forms.Label();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.lstTasks = new System.Windows.Forms.ListBox();
            this.btnViewTasks = new System.Windows.Forms.Button();
            this.btnCompleteTask = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.btnShowHelp = new System.Windows.Forms.Button();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.btnCheckReminders = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.lblTaskDescription = new System.Windows.Forms.Label();
            this.lblUserInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUserInput
            // 
            this.txtUserInput.Location = new System.Drawing.Point(20, 20);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(400, 20);
            this.txtUserInput.TabIndex = 0;
            // 
            // btnAsk
            // 
            this.btnAsk.Location = new System.Drawing.Point(430, 20);
            this.btnAsk.Name = "btnAsk";
            this.btnAsk.Size = new System.Drawing.Size(100, 23);
            this.btnAsk.TabIndex = 1;
            this.btnAsk.Text = "Ask";
            this.btnAsk.UseVisualStyleBackColor = true;
            this.btnAsk.Click += new System.EventHandler(this.btnAsk_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(20, 85);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(510, 95);
            this.lstOutput.TabIndex = 3;
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(20, 50);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 13);
            this.lblResponse.TabIndex = 2;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.Location = new System.Drawing.Point(20, 231);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(250, 20);
            this.txtTaskTitle.TabIndex = 4;
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(20, 278);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(250, 20);
            this.txtTaskDescription.TabIndex = 5;
            this.txtTaskDescription.TextChanged += new System.EventHandler(this.txtTaskDescription_TextChanged);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(276, 228);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(110, 23);
            this.btnAddTask.TabIndex = 6;
            this.btnAddTask.Text = "Add Task";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // lstTasks
            // 
            this.lstTasks.FormattingEnabled = true;
            this.lstTasks.Location = new System.Drawing.Point(20, 304);
            this.lstTasks.Name = "lstTasks";
            this.lstTasks.Size = new System.Drawing.Size(510, 95);
            this.lstTasks.TabIndex = 7;
            // 
            // btnViewTasks
            // 
            this.btnViewTasks.Location = new System.Drawing.Point(20, 405);
            this.btnViewTasks.Name = "btnViewTasks";
            this.btnViewTasks.Size = new System.Drawing.Size(110, 23);
            this.btnViewTasks.TabIndex = 8;
            this.btnViewTasks.Text = "View Tasks";
            this.btnViewTasks.UseVisualStyleBackColor = true;
            this.btnViewTasks.Click += new System.EventHandler(this.btnViewTasks_Click);
            // 
            // btnCompleteTask
            // 
            this.btnCompleteTask.Location = new System.Drawing.Point(276, 405);
            this.btnCompleteTask.Name = "btnCompleteTask";
            this.btnCompleteTask.Size = new System.Drawing.Size(110, 23);
            this.btnCompleteTask.TabIndex = 9;
            this.btnCompleteTask.Text = "Complete Task";
            this.btnCompleteTask.UseVisualStyleBackColor = true;
            this.btnCompleteTask.Click += new System.EventHandler(this.btnCompleteTask_Click);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(420, 405);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(110, 23);
            this.btnDeleteTask.TabIndex = 10;
            this.btnDeleteTask.Text = "Delete Task";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(420, 186);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(110, 23);
            this.btnViewLog.TabIndex = 11;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.UseVisualStyleBackColor = true;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            // 
            // btnShowHelp
            // 
            this.btnShowHelp.Location = new System.Drawing.Point(430, 50);
            this.btnShowHelp.Name = "btnShowHelp";
            this.btnShowHelp.Size = new System.Drawing.Size(100, 23);
            this.btnShowHelp.TabIndex = 12;
            this.btnShowHelp.Text = "Help";
            this.btnShowHelp.UseVisualStyleBackColor = true;
            this.btnShowHelp.Click += new System.EventHandler(this.btnShowHelp_Click);
            // 
            // btnStartQuiz
            // 
            this.btnStartQuiz.Location = new System.Drawing.Point(20, 186);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(100, 23);
            this.btnStartQuiz.TabIndex = 13;
            this.btnStartQuiz.Text = "Start Quiz";
            this.btnStartQuiz.UseVisualStyleBackColor = true;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);
            // 
            // btnCheckReminders
            // 
            this.btnCheckReminders.Location = new System.Drawing.Point(276, 186);
            this.btnCheckReminders.Name = "btnCheckReminders";
            this.btnCheckReminders.Size = new System.Drawing.Size(110, 23);
            this.btnCheckReminders.TabIndex = 14;
            this.btnCheckReminders.Text = "Check Reminders";
            this.btnCheckReminders.UseVisualStyleBackColor = true;
            this.btnCheckReminders.Click += new System.EventHandler(this.btnCheckReminders_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "CyberKnight";
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Location = new System.Drawing.Point(17, 215);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(54, 13);
            this.lblTaskTitle.TabIndex = 16;
            this.lblTaskTitle.Text = "Task Title";
            // 
            // lblTaskDescription
            // 
            this.lblTaskDescription.AutoSize = true;
            this.lblTaskDescription.Location = new System.Drawing.Point(20, 262);
            this.lblTaskDescription.Name = "lblTaskDescription";
            this.lblTaskDescription.Size = new System.Drawing.Size(87, 13);
            this.lblTaskDescription.TabIndex = 17;
            this.lblTaskDescription.Text = "Task Description";
            // 
            // lblUserInput
            // 
            this.lblUserInput.AutoSize = true;
            this.lblUserInput.Location = new System.Drawing.Point(20, 4);
            this.lblUserInput.Name = "lblUserInput";
            this.lblUserInput.Size = new System.Drawing.Size(60, 13);
            this.lblUserInput.TabIndex = 18;
            this.lblUserInput.Text = "Type Here:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 432);
            this.Controls.Add(this.lblUserInput);
            this.Controls.Add(this.lblTaskDescription);
            this.Controls.Add(this.lblTaskTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.btnAsk);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.txtTaskTitle);
            this.Controls.Add(this.txtTaskDescription);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.lstTasks);
            this.Controls.Add(this.btnViewTasks);
            this.Controls.Add(this.btnCompleteTask);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.btnViewLog);
            this.Controls.Add(this.btnShowHelp);
            this.Controls.Add(this.btnStartQuiz);
            this.Controls.Add(this.btnCheckReminders);
            this.Name = "MainForm";
            this.Text = "CyberKnight";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnAsk;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.ListBox lstTasks;
        private System.Windows.Forms.Button btnViewTasks;
        private System.Windows.Forms.Button btnCompleteTask;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnViewLog;
        private System.Windows.Forms.Button btnShowHelp;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.Button btnCheckReminders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTaskTitle;
        private System.Windows.Forms.Label lblTaskDescription;
        private System.Windows.Forms.Label lblUserInput;
    }
}
