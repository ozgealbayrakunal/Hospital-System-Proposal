using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HospitalSystemProposal
{
    public partial class AlgologyForm : Form
    {
        SqlConnection baglanti, baglanti2;
        SqlCommand komut, komut2;
        SqlDataAdapter da;

        public AlgologyForm()
        {
            InitializeComponent();
        }
        public string patientname,
    patientsurname,
    tcno;

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void AlgologyForm_Load(object sender, EventArgs e)
        {
            patientname = Form1.patient_name;
            patient_surname = Form1.patient_surname;
            tcno = Form1.tc_no;
            polyclinicid = Form1.polyclinicId;

            
                button1.BackColor = Color.Pink;
                doctorno = 1;          
            

            baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
            string sorgu = "Insert into DOKTORTABLE (polyclinicID,doctorno,patientname,patientsurname,PatientTC) values (@polyclinicid,@doctorno,@patientname,@patientsurname,@tcno)";
            komut = new SqlCommand(sorgu);
            baglanti.Open();
            komut.Connection = baglanti;

            komut.Parameters.AddWithValue("@polyclinicid", polyclinicid);
            komut.Parameters.AddWithValue("@doctorno", doctorno);
            komut.Parameters.AddWithValue("@patientname", patientname);
            komut.Parameters.AddWithValue("@patientsurname", patientsurname);
            komut.Parameters.AddWithValue("@tcno", tcno);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        public int polyclinicid, doctorno;
        public static int siraNo;
        private void button1_Click(object sender, EventArgs e)
        {
            int sayac1 = 0, sayac2 = 0, sayac3 = 0, sayac4 = 0, sayac5 = 0, sayac6 = 0;

            Random rand1 = new Random();
            double u1 = rand1.NextDouble();
            double u2 = rand1.NextDouble();
            double stdDev = 0.223528189;
            double mean = 0.166;
            double randStdNormal1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double rastgeleSayi1 = mean + stdDev * randStdNormal1;

            baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT ID FROM DOKTORTABLE WHERE PatientTC=@tcno";
            komut.Parameters.AddWithValue("@tcno", tcno);
            baglanti.Open();
            int hastID = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
            baglanti.Close();


            if (0 <= rastgeleSayi1 && rastgeleSayi1 < 0.08861671)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM ECG";//NULL içermeyen roomNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM ECG";//NULL içermeyen roomNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM ECG";//NULL içermeyen roomNo3 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 30);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //roomNo1
                    {
                        //ECG tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ECGEkle = "Insert into Ecg (patientID, roomNo1, roomNo2, roomNo3) values (@patientId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ECGEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //roomNo2
                    {
                        //Ecg tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ECGEkle = "Insert into Ecg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ECGEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //oroomNo3
                    {
                        //Ecg tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ecgEkle = "Insert into ECG (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ecgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3 };

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi[min] )
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //Ecg tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ecgEkle = "Insert into ECG (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ecgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }
                    else if (minimum == roomNo2)
                    {
                        //ecg  tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ecgEkle = "Insert into ecg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ecgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }
                    else
                    {
                        //CT tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ecgEkle = "Insert into Ecg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ecgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac1++;
                    }

                }
            }
            if (0.08861671 <= rastgeleSayi1 && rastgeleSayi1 < 0.17723343)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM CUA";//NULL içermeyen roomNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM CUA";//NULL içermeyen roomNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM CUA";//NULL içermeyen roomNo3 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM CUA";//NULL içermeyen roomNo4 sayısı
                baglanti.Open();
                Int32 roomNo4 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0 && roomNo4 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 40);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //roomNo1
                    {
                        //CUA tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, 1, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //roomNo2
                    {
                        //CUAtablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //roomNo3
                    {
                        //CUA tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else //roomNo4
                    {
                        // CUA tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital
                        ;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3, roomNo4 };

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi [min])
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, 1, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else if (minimum == roomNo2)
                    {
                        //X-Ray tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else if (minimum == roomNo3)
                    {
                        //X-Ray tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;
                    }
                    else
                    {
                        //X-Ray tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string cuaEkle = "Insert into CUA (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(cuaEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac2++;

                    }
                }

            }
            if (0.17723343 <= rastgeleSayi1 && rastgeleSayi1 < 0.218299712)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM CT";//NULL içermeyen roomNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM CT";//NULL içermeyen roomNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM CT";//NULL içermeyen roomNo3 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 30);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //roomNo1
                    {
                        //CT tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //roomNo2
                    {
                        //X-Ray tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //roomNo3
                    {
                        //CT tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3 };

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi[min])
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //X-Ray tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }
                    else if (minimum == roomNo2)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }
                    else
                    {
                        //CT tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ctEkle = "Insert into CT (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ctEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac3++;
                    }

                }
            }
            if (0.218299712 <= rastgeleSayi1 && rastgeleSayi1 < 0.22478386)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM Blood";//NULL içermeyen roomNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM Blood";//NULL içermeyen roomNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM Blood";//NULL içermeyen roomNo3 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo4) FROM Blood";//NULL içermeyen roomNo4 sayısı
                baglanti.Open();
                Int32 roomNo4 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0 && roomNo4 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 40);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //roomNo1
                    {
                        //Blood tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, 1, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //roomNo2
                    {
                        //
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //roomNo3
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else //oroomNo4
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3, roomNo4 };

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi [min])
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //X-Ray 
                        
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string loodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, 1, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else if (minimum == roomNo2)
                    {
                        // 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else if (minimum == roomNo3)
                    {
                        // 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;
                    }
                    else
                    {
                        //X-Ray 
                
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string bloodEkle = "Insert into Blood (patientID, roomNo1, roomNo2, roomNo3, roomNo4) values (@hastId, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(bloodEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac4++;

                    }
                }

            }
            if (0.22478386 <= rastgeleSayi1 && rastgeleSayi1 < 0.268011152)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM Usg";//NULL içermeyen odaNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM Usg";//NULL içermeyen odaNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM Usg";//NULL içermeyen odaNo2 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 30);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //oda1
                    {
                        //X-Ray 
                    
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //oroomNo2
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //roomNo3
                    {
                        //usg tablosnuna gönderilicel
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3};

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi[min])
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;
                    }
                    else if (minimum == roomNo2)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, 1, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;
                    }
                    else
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string usgEkle = "Insert into Usg (patientID, roomNo1, roomNo2, roomNo3) values (@hastId, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(usgEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac5++;

                    }
                }

            }
            if (0.268011152 <= rastgeleSayi1 && rastgeleSayi1 < 1)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo1) FROM X-Ray";//NULL içermeyen roomNo1 sayısı
                baglanti.Open();
                Int32 roomNo1 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo2) FROM X-Ray";//NULL içermeyen roomNo2 sayısı
                baglanti.Open();
                Int32 roomNo2 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo3) FROM X-Ray";//NULL içermeyen roomNo3 sayısı
                baglanti.Open();
                Int32 roomNo3 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo4) FROM X-Ray";//NULL içermeyen roomNo4 sayısı
                baglanti.Open();
                Int32 roomNo4 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo5) FROM X-Ray";//NULL içermeyen odaNo2 sayısı
                baglanti.Open();
                Int32 roomNo5 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT Count(roomNo6) FROM X-Ray";//NULL içermeyen odaNo2 sayısı
                baglanti.Open();
                Int32 roomNo6 = (Int32)komut.ExecuteScalar();
                baglanti.Close();

                if (roomNo1 == 0 && roomNo2 == 0 && roomNo3 == 0 && roomNo4 == 0 && roomNo5 == 0 && roomNo6 == 0)
                {
                    //rastgele gonder
                    Random rand3 = new Random();
                    double rastgele1 = rand3.Next(1, 60);
                    if (rastgele1 >= 1 && rastgele1 <= 10) //oda1
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, 1, NULL, NULL, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (rastgele1 >= 10 && rastgele1 <= 20) //oda2
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, 1, NULL, NULL,NULL,NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (rastgele1 >= 20 && rastgele1 <= 30) //oda3
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, 1,NULL,NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (rastgele1 >= 30 && rastgele1 <= 40) //oda4
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (rastgele1 >= 40 && rastgele1 <= 50) //oda5
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, NULL, 1 NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }

                    else //odaNo6
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                }

                else
                {
                    int[] dizi = { roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6 };

                    for (int i = 0; i < dizi.Length - 1; i++)
                    {
                        int min = i;
                        for (int j = i + 1; j < dizi.Length; j++)
                        {
                            if (dizi[j] < dizi[min] )
                            {
                                min = j;
                            }
                        }
                        int temp = dizi[i];
                        dizi[i] = dizi[min];
                        dizi[min] = temp;
                    }

                    int minimum = dizi[0];

                    //sayı olarak az olana gonder
                    if (minimum == roomNo1)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, 1, NULL, NULL, NULL, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (minimum == roomNo2)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, 1, NULL, NULL,NULL,NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else if (minimum == roomNo3)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, 1,NULL,NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;


                    }
                    else if (minimum == roomNo4)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, 1, NULL, NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }

                    else if (minimum == roomNo5)
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, NULL, 1 NULL)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                    else
                    {
                        //X-Ray 
                        baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                        //   da=new SqlDataAdapter
                        string ronEkle = "Insert into X-Ray (patientID, roomNo1, roomNo2, roomNo3, roomNo4, roomNo5, roomNo6) values (@hastId, NULL, NULL, NULL, NULL, NULL, 1)";
                        //komut = new SqlCommand(sorgu,baglanti);
                        komut = new SqlCommand(ronEkle);
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.Parameters.AddWithValue("@hastId", hastID);
                        //baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        sayac6++;
                    }
                }

            }

            if (sayac1 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM Ecg";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();

                listBox2.Items.Add("Ecg Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo - 1 ) * 5);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "Ecg";
                int sure = (siraNo - 1 ) * 5;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            if (sayac2 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM CUA";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();

                listBox2.Items.Add("CUA Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo - 1) * 5);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "CUA";
                int sure = (siraNo - 1) * 5;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            if (sayac3 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM CT";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();

                listBox2.Items.Add("CT Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo - 1 ) * 30);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "CT";
                int sure = (siraNo - 1 ) * 30;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            if (sayac4 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM Usg";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();
                listBox2.Items.Add("Usg Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo - 1 ) * 40);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "Usg";
                int sure = (siraNo - 1 ) * 40;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            if (sayac5 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM X-Ray";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();
                listBox2.Items.Add("X-Ray Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo - 1) * 30);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "X-Ray";
                int sure = (siraNo - 1 ) * 30;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            if (sayac6 != 0)
            {
                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT COUNT(*) FROM Blood";
                baglanti.Open();
                siraNo = (Int32)komut.ExecuteScalar(); //sorgu sonucu geriye tek değer dönecekse "ExecuteScalar" kullanılır.
                baglanti.Close();
                listBox2.Items.Add("Blood Hasta Sıra No");
                listBox1.Items.Add((siraNo));

                listBox3.Items.Add("Tahmini Bekleme Süresi");
                listBox4.Items.Add((siraNo  - 1 ) * 3);

                baglanti = new SqlConnection(@"Data Source=DESKTOP-CQ016AR;Initial Catalog=hospital;Integrated Security=True");
                string sorgu = "Insert into Sure (Sure,TetkikveTahlil) values (@sure,@tetkikvetahlil)";
                komut = new SqlCommand(sorgu);
                baglanti.Open();
                komut.Connection = baglanti;
                string tetkikvetahlil = "Blood";
                int sure = (siraNo - 1 ) * 3;
                komut.Parameters.AddWithValue("@sure", sure);
                komut.Parameters.AddWithValue("@tetkikvetahlil", tetkikvetahlil);
                komut.ExecuteNonQuery();
                baglanti.Close();
            }


        }
    }
    }

