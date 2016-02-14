namespace FTetris.WinForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.nextPolyominoStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.scoreStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gameBoardView = new FTetris.WinForm.GameBoardView();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextPolyominoStatusLabel,
            this.scoreStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 0);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(248, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 1;
            // 
            // nextPolyominoStatusLabel
            // 
            this.nextPolyominoStatusLabel.Name = "nextPolyominoStatusLabel";
            this.nextPolyominoStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // scoreStatusLabel
            // 
            this.scoreStatusLabel.Name = "scoreStatusLabel";
            this.scoreStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // gameBoardView
            // 
            this.gameBoardView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameBoardView.BackColor = System.Drawing.Color.Black;
            this.gameBoardView.DataContext = null;
            this.gameBoardView.Location = new System.Drawing.Point(0, 31);
            this.gameBoardView.Margin = new System.Windows.Forms.Padding(5);
            this.gameBoardView.Name = "gameBoardView";
            this.gameBoardView.Size = new System.Drawing.Size(248, 647);
            this.gameBoardView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 678);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.gameBoardView);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "FTetris(Enter: Start ←: Left →: Right ↑: Turn Right ↓: Turn Left Space: Drop)";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GameBoardView gameBoardView;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel nextPolyominoStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel scoreStatusLabel;
    }
}

