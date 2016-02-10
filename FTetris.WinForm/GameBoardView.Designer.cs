namespace FTetris.WinForm
{
    partial class GameBoardView
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameBoardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GameBoardView";
            this.Size = new System.Drawing.Size(200, 188);
            this.Load += new System.EventHandler(this.OnLoad);
            this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviousKeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
