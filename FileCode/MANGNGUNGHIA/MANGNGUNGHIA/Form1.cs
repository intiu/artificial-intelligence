using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANGNGUNGHIA
{
    public partial class Form1 : Form
    {
        #region Điều Khiển
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void Tính_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            stop = 0;
            ketqua.Text = string.Empty;
            if (giatritinh.SelectedItem == giatritinh.Items[0] || NgoaiLe() == 1 || NgoaiLe() == 2)
            {
                if (giatritinh.SelectedItem == giatritinh.Items[0])
                    MessageBox.Show("Chưa chọn giá trị tính!!", "Cảnh Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (NgoaiLe() == 1 || NgoaiLe() == 2)
                    MessageBox.Show("- Giá trị nhập vào không thỏa mãn điều kiện của 1 tam giác.\n- Vui lòng nhập lại!.", "Cảnh Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Xuly();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            giatritinh.SelectedItem = giatritinh.Items[0];
            this.Width = 472;
            this.Height = 667;
            Arr();
            HienThiDGV();
            pictureBox1.Refresh();
            goc_a.Text = goc_b.Text = goc_c.Text = string.Empty;
            canh_a.Text = canh_b.Text = canh_c.Text = string.Empty;
            dientich.Text = chieucao.Text = string.Empty;
            ketqua.Text = string.Empty;
            if (bd == 1)
            {
                this.Left += 350;
                bd = 0;
            }
        }

        private void Trogiup_Click(object sender, EventArgs e)
        {
            string notice = "- Nhập các giá trị thuộc tính của tam giác ở các ô.";
            string notice2 = "\n- Chọn Giá Trị Cần Tính ở combobox.";
            string notice3 = "\n- Nhấn nút Tính và xem kết quả bạn muốn xem. ";
            string notice4 = "\n- Nhấn nút Làm Lại để nhập một bài toán mới để xử lý. ";
            string notice5 = "\n- Bảng Quan Hệ: Thể hiện giữa yếu tố và công thức\n\t  1 :Giá trị của yếu tố đã được kích hoạt(đã tính)\n\t-1 :Giá trị của yếu tố chưa được kích hoạt(chưa được tính)";
            string notice6 = "\n- Sơ Đồ Quan Hệ: Thể hiện trực quan mối quan hệ giữa công thức và yếu tố ";
            string notice7 = "\n- Trường hợp không tính được kết quả. Xin vui lòng nhập thêm yếu tố để tính toán.";
            MessageBox.Show(notice + notice2 + notice3 + notice4 + notice5 + notice6 + notice7, "Trợ giúp sử dụng chương trình!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

#endregion

        #region Khai Báo Biến

        public string[] YEUTO = { "Góc alpha", "Góc beta", "Góc delta", "Cạnh a", "Cạnh b", "Cạnh c", "Chu Vi", "Diện Tích", "Chiều Cao(hc)" };
        private float congthuc = 6, yeuto = 9;
        private float[,] a = new float[9, 6];
        private const float goc = 180f;
        private float kq = 0;
        private float[,] ArrLuu = new float[9, 6];
        private int stop;
        bool flag;
        Pen Mypen = new Pen(Color.Green, 4);
        Timer mytime = new Timer();
        int bd;

        #endregion  

        #region Khởi Tạo Mảng
        private void Arr()
        {
            float temp = -1;
            for (int i = 0; i < yeuto; i++)
                for (int j = 0; j < congthuc; j++)
                {
                    a[i, j] = 0;
                    ArrLuu[i, j] = 0;
                }
            //Mảng xử lý
            a[0, 0] = a[0, 3] = temp;
            a[1, 0] = a[1, 1] = a[1, 3] = temp;
            a[2, 1] = a[2, 3] = temp;
            a[3, 0] = a[3, 2] = a[3, 5] = temp;
            a[4, 0] = a[4, 1] = a[4, 2] = a[4, 5] = temp;
            a[5, 1] = a[5, 2] = a[5, 4] = a[5, 5] = temp;
            a[6, 2] = a[6, 5] = temp;
            a[7, 2] = a[7, 4] = temp;
            a[8, 4] = temp;
            //Mảng xuất ra DataGridView
            ArrLuu[0, 0] = ArrLuu[0, 3] = temp;
            ArrLuu[1, 0] = ArrLuu[1, 1] = ArrLuu[1, 3] = temp;
            ArrLuu[2, 1] = ArrLuu[2, 3] = temp;
            ArrLuu[3, 0] = ArrLuu[3, 2] = ArrLuu[3, 5] = temp;
            ArrLuu[4, 0] = ArrLuu[4, 1] = ArrLuu[4, 2] = ArrLuu[4, 5] = temp;
            ArrLuu[5, 1] = ArrLuu[5, 2] = ArrLuu[5, 4] = ArrLuu[5, 5] = temp;
            ArrLuu[6, 2] = ArrLuu[6, 5] = temp;
            ArrLuu[7, 2] = ArrLuu[7, 4] = temp;
            ArrLuu[8, 4] = temp;
        }
        #endregion

        #region Xử Lý

        //Kiểm tra xem giá trị ở combobox đã được tính chưa.
        private bool Tinhgiatri()
        {
            switch (giatritinh.SelectedIndex)
            {
                case 1:
                    if (a[0, 0] == -1)
                    {
                        break;
                    }
                    return true;

                case 2:
                    if (a[1, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 3:
                    if (a[2, 1] == -1)
                    {
                        break;
                    }
                    return true;
                case 4:
                    if (a[3, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 5:
                    if (a[4, 0] == -1)
                    {
                        break;
                    }
                    return true;
                case 6:
                    if (a[5, 1] == -1)
                    {
                        break;
                    }
                    return true;
                case 7:
                    if (a[7, 2] == -1)
                    {
                        break;
                    }
                    return true;
                case 8:
                    if (a[8, 4] == -1)
                    {
                        break;
                    }
                    return true;
                case 9:
                    if (a[6, 2] == -1)
                    {
                        break;
                    }
                    return true;
            }
            return false;
        }
        //Hiển thị lên Textbox kết quả.
        public void HienThiKetQua()
        {
            switch (giatritinh.SelectedIndex)
            {
                case 1:
                    kq = (float)((a[0, 0] * goc) / Math.PI);
                    ketqua.Text = kq.ToString();
                    break;

                case 2:
                    kq = (float)((a[1, 0] * goc) / Math.PI);
                    ketqua.Text = kq.ToString();
                    break;

                case 3:
                    kq = (float)((a[2, 1] * goc) / Math.PI);
                    ketqua.Text = kq.ToString();
                    break;

                case 4:
                    ketqua.Text = a[3, 0].ToString();
                    break;

                case 5:
                    ketqua.Text = a[4, 0].ToString();
                    break;

                case 6:
                    ketqua.Text = a[5, 1].ToString();
                    break;

                case 7:
                    ketqua.Text = a[7, 2].ToString();
                    break;

                case 8:
                    ketqua.Text = a[8, 4].ToString();
                    break;

                case 9:
                    kq = a[6, 2] * 2;
                    ketqua.Text = kq.ToString();
                    break;
            }
        }
        //Phương thức này sẽ kiểm tra xem yếu tố nào có thể tính.
        private int LayYeuTo(int congthuc1)
        {
            int dem = 0, gt = -1;
            for (int i = 0; i < yeuto; i++)
                if (a[i, congthuc1] == -1)
                {
                    dem++;
                    gt = i;
                }
            if (dem == 1)
                return gt;
            return -1;
        }
        //Kích hoạt cơ chế lan truyền.
        private void Cochelantruyen(int congthuc1, int yeuto1)
        {
            float value = -1, lgt = -1;
            switch (congthuc1)
            {
                case 0:
                    switch (yeuto1)
                    {
                        case 0:
                            lgt = (float)((a[3, 0] * Math.Sin(a[1, 0])) / (a[4, 0]));
                            value = (float)Math.Asin(lgt);
                            break;
                        case 1:
                            lgt = (float)((a[4, 0] * Math.Sin(a[0, 0])) / (a[3, 0]));
                            value = (float)Math.Asin(lgt);
                            break;
                        case 3:
                            value = (float)((a[4, 0] * Math.Sin(a[0, 0])) / Math.Sin(a[1, 0]));
                            break;
                        case 4:
                            value = (float)((a[3, 0] * Math.Sin(a[1, 0])) / Math.Sin(a[0, 0]));
                            break;
                    }
                    break;
                case 1:
                    switch (yeuto1)
                    {
                        case 1:
                            lgt = (float)((a[4, 0] * Math.Sin(a[2, 1])) / (a[5, 1]));
                            value = (float)Math.Asin(lgt);
                            break;
                        case 2:
                            lgt = (float)((a[5, 1] * Math.Sin(a[1, 1])) / a[4, 1]);
                            value = (float)Math.Asin(lgt);
                            break;
                        case 4:
                            value = (float)((a[5, 1] * Math.Sin(a[1, 0])) / Math.Sin(a[2, 1]));
                            break;
                        case 5:
                            value = (float)((a[4, 0] * Math.Sin(a[2, 1])) / Math.Sin(a[1, 0]));
                            break;
                    }
                    break;
                case 2:
                    float nuachuvi = (float)((a[3, 0] + a[4, 0] + a[5, 1]) / 2);
                    switch (yeuto1)
                    {
                        case 3:
                            value = (float)(nuachuvi - (Math.Pow(a[7, 2], 2.0) / (nuachuvi * (nuachuvi - a[4, 0]) * (nuachuvi - a[5, 1]))));
                            break;
                        case 4:
                            value = (float)(nuachuvi - (Math.Pow(a[7, 2], 2.0) / (nuachuvi * (nuachuvi - a[3, 0]) * (nuachuvi - a[4, 1]))));
                            break;
                        case 5:
                            value = (float)(nuachuvi - (Math.Pow(a[7, 2], 2.0) / (nuachuvi * (nuachuvi - a[4, 1]) * (nuachuvi - a[5, 1]))));
                            break;
                        case 6:
                            value = (float)(a[3, 0] + a[4, 0] + a[5, 1]);
                            break;
                        case 7:
                            value = (float)Math.Sqrt((double)(nuachuvi * (nuachuvi - a[3, 0]) * (nuachuvi - a[4, 0]) * (nuachuvi - a[5, 1])));
                            break;
                    }
                    break;
                case 3:
                    switch (yeuto1)
                    {
                        case 0:
                            value = (float)((Math.PI - a[1, 0] - a[2, 1]));
                            break;
                        case 1:
                            value = (float)((Math.PI - a[0, 0] - a[2, 1]));
                            break;
                        case 2:
                            value = (float)((Math.PI - a[0, 0] - a[1, 0]));
                            break;
                    }
                    break;
                case 4:
                    switch (yeuto1)
                    {
                        case 5:
                            value = (float)(2f * a[7, 2] / a[8, 4]); ;
                            break;
                        case 7:
                            value = (float)((a[7, 4] * a[5, 1]) / 2f);
                            break;
                        case 8:
                            value = (float)(2f * a[7, 2] / a[5, 1]);
                            break;
                    }
                    break;
                case 5:
                    switch (yeuto1)
                    {
                        case 3:
                            value = (float)(2f * a[6, 2] - a[4, 0] - a[5, 1]);
                            break;
                        case 4:
                            value = (float)(2f * a[6, 2] - a[3, 0] - a[5, 1]);
                            break;
                        case 5:
                            value = (float)(2f * a[6, 2] - a[3, 0] - a[4, 0]);
                            break;
                        case 6:
                            value = (float)((a[3, 0] + a[4, 0] + a[5, 1]) / 2);
                            break;
                    }
                    break;
            }
            if (value <= 0)
            {
                MessageBox.Show("Các yếu tố nhập vào không hợp lệ!!. Vui lòng kiểm tra lại.", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                stop = 1;
            }
            else
            {
                for (int i = 0; i < congthuc; i++)
                    if (a[yeuto1, i] == -1)
                    {
                        a[yeuto1, i] = value;
                        ArrLuu[yeuto1, i] = 1;
                    }
            }
        }
        //Xử lý giá trị truyền vào từ textbox
        private void LayGiaTri()
        {
            Arr();
            if (!string.IsNullOrEmpty(goc_a.Text))
            {
                float num = ((float)Math.PI * float.Parse(goc_a.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[0, i] == -1f && this.a[0, i] != 0)
                    {
                        this.a[0, i] = num;
                        this.ArrLuu[0, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(goc_b.Text))
            {
                float num1 = ((float)Math.PI * float.Parse(goc_b.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[1, i] == -1f && this.a[1, i] != 0)
                    {
                        this.a[1, i] = num1;
                        this.ArrLuu[1, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(goc_c.Text))
            {
                float num2 = ((float)Math.PI * float.Parse(goc_c.Text)) / goc;
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[2, i] == -1f && this.a[2, i] != 0)
                    {
                        this.a[2, i] = num2;
                        this.ArrLuu[2, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(canh_a.Text))
            {
                float num3 = float.Parse(canh_a.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[3, i] == -1f && this.a[3, i] != 0)
                    {
                        this.a[3, i] = num3;
                        this.ArrLuu[3, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(canh_b.Text))
            {
                float num4 = float.Parse(canh_b.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[4, i] == -1f && this.a[4, i] != 0)
                    {
                        this.a[4, i] = num4;
                        this.ArrLuu[4, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(canh_c.Text))
            {
                float num5 = float.Parse(canh_c.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[5, i] == -1f && this.a[5, i] != 0)
                    {
                        this.a[5, i] = num5;
                        this.ArrLuu[5, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(dientich.Text))
            {
                float num6 = float.Parse(dientich.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[7, i] == -1f && this.a[7, i] != 0)
                    {
                        this.a[7, i] = num6;
                        this.ArrLuu[7, i] = 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(chieucao.Text))
            {
                float num7 = float.Parse(chieucao.Text);
                for (int i = 0; i < congthuc; i++)
                {
                    if (this.a[8, i] == -1f && this.a[8, i] != 0)
                    {
                        this.a[8, i] = num7;
                        this.ArrLuu[8, i] = 1;
                    }
                }
            }
        }
        //Tính toán
        private void Xuly()
        {
            flag = true;
            LayGiaTri();
            while (flag == true)
            {
                flag = false;
                for (int i = 0; i < congthuc; i++)
                {
                    int layyeuto = LayYeuTo(i);
                    if (layyeuto != -1)
                    {
                        if (stop == 1)
                            break;
                        Cochelantruyen(i, layyeuto);
                        flag = true;
                        if (Tinhgiatri())
                        {
                            Form1_Resize();
                            HienThiKetQua();
                            mytime.Enabled = true;
                            mytime.Interval = 500;
                            mytime.Tick += new EventHandler(VeMoiLienHe);
                            flag = false;
                            break;
                        }
                    }
                }
            }
            if (!Tinhgiatri() && stop == 0)
                MessageBox.Show("- Không đủ yếu tố !.\n- Không thể tính kết quả trên mạng ngữ nghĩa đã xây dựng !!.\n- Vui Lòng Xem Trợ Giúp !!", "Báo Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            HienThiDGV();
        }
        //Vẽ lên mạng ngữ nghĩa
        public void VeMoiLienHe(object sender, EventArgs e)
        {
            if (a[0, 0] != -1)
            {
                g.DrawLine(Mypen, 350, 58, 350, 88);
                g.DrawLine(Mypen, 220, 210, 320, 140);
                g.DrawLine(Mypen, 465, 205, 380, 145);
            }
            if (a[1, 0] != -1)
            {
                g.DrawLine(Mypen, 150, 88, 230, 58);
                g.DrawLine(Mypen, 140, 210, 140, 155);
            }
            if (a[2, 1] != -1)
            {
                g.DrawLine(Mypen, 560, 88, 480, 58);
                g.DrawLine(Mypen, 560, 205, 560, 157);
            }
            if (a[3, 0] != -1)
            {
                g.DrawLine(Mypen, 90, 278, 80, 335);
                g.DrawLine(Mypen, 80, 400, 90, 470);
                g.DrawLine(Mypen, 90, 345, 253, 293);
            }
            if (a[4, 0] != -1)
            {
                g.DrawLine(Mypen, 180, 278, 190, 335);
                g.DrawLine(Mypen, 195, 400, 170, 470);
                g.DrawLine(Mypen, 215, 353, 266, 300);
                g.DrawBezier(Mypen, 215, 391, 320, 465, 410, 395, 470, 278);
            }
            if (a[5, 2] != -1)
            {
                g.DrawLine(Mypen, 485, 278, 475, 335);
                g.DrawLine(Mypen, 485, 400, 495, 470);
                g.DrawLine(Mypen, 445, 390, 335, 470);
                g.DrawLine(Mypen, 440, 355, 393, 300);
            }
            if (a[6, 2] != -1)
            {
                g.DrawLine(Mypen, 310, 300, 310, 335);
                g.DrawLine(Mypen, 315, 405, 300, 470);
            }
            if (a[7, 2] != -1)
            {
                g.DrawLine(Mypen, 330, 518, 402, 552);
                g.DrawLine(Mypen, 495, 520, 442, 554);
            }
            if (a[8, 4] != -1)
            {
                g.DrawLine(Mypen, 610, 400, 590, 470);
            }
        }
        //Hiển thị DataGridView
        private void HienThiDGV()
        {
            BangSuyDien.Rows.Clear();
            if (BangSuyDien.RowCount == 0)
                BangSuyDien.Rows.Add(10);
            for (int i = 0; i < yeuto; i++)
            {
                for (int j = 0; j <= congthuc; j++)
                {
                    if (j == 0)
                    {
                        BangSuyDien[j, i].Value = YEUTO[i];
                        BangSuyDien[j, i].Style.ForeColor = Color.Green;
                    }
                    else
                    {
                        BangSuyDien[j, i].Value = ArrLuu[i, j - 1];
                        if (ArrLuu[i, j - 1] == -1)
                            BangSuyDien[j, i].Style.BackColor = Color.LightSkyBlue;
                        if (ArrLuu[i, j - 1] == 1)
                            BangSuyDien[j, i].Style.BackColor = Color.Yellow;
                    }
                }
            }
        }

        //Thay đổi Form
        private void Form1_Resize()
        {
            this.Width = 1165;
            this.Height = 667;
            if (bd == 0)
                this.Left -= 350;
            bd = 1;

        }
        //Xử lý ngoại lệ. Điều kiện của một tam giác.
        private int NgoaiLe()
        {
            int kiemtra = 0;
            if (!string.IsNullOrEmpty(goc_a.Text) && !string.IsNullOrEmpty(goc_b.Text) && !string.IsNullOrEmpty(goc_c.Text))
                if (float.Parse((goc_a.Text)) + float.Parse((goc_b.Text)) + float.Parse((goc_c.Text)) != 180)
                    kiemtra = 1;

            if (!string.IsNullOrEmpty(canh_a.Text) && !string.IsNullOrEmpty(canh_b.Text) && !string.IsNullOrEmpty(canh_c.Text))
                if ((float.Parse((canh_a.Text)) + float.Parse((canh_b.Text)) <= float.Parse((canh_c.Text))) || (float.Parse((canh_a.Text)) + float.Parse((canh_c.Text)) <= float.Parse((canh_b.Text))) || (float.Parse((canh_b.Text)) + float.Parse((canh_c.Text)) <= float.Parse((canh_a.Text))))
                    kiemtra = 2;
            return kiemtra;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            giatritinh.SelectedItem = giatritinh.Items[0];
            Arr();
            HienThiDGV();
        }

        private void goc_a_TextChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Textbox
        //Chỉ cho phép nhập số vào textbox
        private void Chinhapso(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && ((int)e.KeyChar != 46))
            {
                e.Handled = true;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    int ma = (int)e.KeyChar;
                    if ((ma == 26) || (ma == 24) || (ma == 3) || (ma == 22) || (ma == 1))
                        e.Handled = true;
                }
            }
        }
        #endregion
    }
}
