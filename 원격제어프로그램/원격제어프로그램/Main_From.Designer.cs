
namespace 원격제어프로그램
{
    partial class Main_From
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbox_IP = new System.Windows.Forms.TextBox();
            this.tbox_controller_IP = new System.Windows.Forms.TextBox();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.timer_send_img = new System.Windows.Forms.Timer(this.components);
            this.noti = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "원격 호스트 주소 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "원격 컨트롤러 주소 :";
            // 
            // tbox_IP
            // 
            this.tbox_IP.Location = new System.Drawing.Point(286, 63);
            this.tbox_IP.Name = "tbox_IP";
            this.tbox_IP.Size = new System.Drawing.Size(309, 28);
            this.tbox_IP.TabIndex = 2;
            // 
            // tbox_controller_IP
            // 
            this.tbox_controller_IP.Location = new System.Drawing.Point(286, 113);
            this.tbox_controller_IP.Name = "tbox_controller_IP";
            this.tbox_controller_IP.ReadOnly = true;
            this.tbox_controller_IP.Size = new System.Drawing.Size(309, 28);
            this.tbox_controller_IP.TabIndex = 3;
            // 
            // btn_Setting
            // 
            this.btn_Setting.Location = new System.Drawing.Point(633, 62);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(170, 32);
            this.btn_Setting.TabIndex = 4;
            this.btn_Setting.Text = "설정하기";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Enabled = false;
            this.btn_ok.Location = new System.Drawing.Point(633, 112);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(170, 33);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "원격 제어 허용";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // timer_send_img
            // 
            this.timer_send_img.Tick += new System.EventHandler(this.timer_send_img_Tick);
            // 
            // noti
            // 
            this.noti.Text = "notifyIcon1";
            this.noti.Visible = true;
            this.noti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.noti_MouseDoubleClick);
            // 
            // Main_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 232);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_Setting);
            this.Controls.Add(this.tbox_controller_IP);
            this.Controls.Add(this.tbox_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Main_From";
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_From_FormClosed);
            this.Load += new System.EventHandler(this.Main_From_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbox_IP;
        private System.Windows.Forms.TextBox tbox_controller_IP;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Timer timer_send_img;
        private System.Windows.Forms.NotifyIcon noti;
    }
}

