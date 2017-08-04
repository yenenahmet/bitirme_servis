using Bitirme_Ekonomistler.Models.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Bitirme_Ekonomistler.Controllers
{
    public class GenelController : ApiController
    {
        SqlConnection myConnection = new SqlConnection("Data Source=YENEN;Initial Catalog=EkonomistlerS;Integrated Security=True");

        [Route("getGörevtamamlamabegeniyorum/{userid}")]
        [HttpGet]
        public List<GörevTamamlamaBegeniYorum> getGorevtamamlamaBegeniyorum(int userid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GörevtamamlanmaBegeniYorum";
            sqlCmd.Parameters.Add("@Userid", SqlDbType.Int);
            sqlCmd.Parameters["@Userid"].Value = userid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            GörevTamamlamaBegeniYorum gorev = new GörevTamamlamaBegeniYorum();
            List<GörevTamamlamaBegeniYorum> gorevlist = new List<GörevTamamlamaBegeniYorum>();
            while (reader.Read())
            {
                gorev = new GörevTamamlamaBegeniYorum();
                try
                {
                    gorev.YorumSayisi = Convert.ToInt32(reader.GetValue(0)).ToString();
                    gorev.BegeniSayisi = Convert.ToInt32(reader.GetValue(1)).ToString();
                }
                catch (Exception)
                {
                    gorev.YorumSayisi = "0";
                    gorev.BegeniSayisi = "0";
                }
                gorev.UserId = Convert.ToInt32(reader.GetValue(2)).ToString();
                gorev.KullaniciAdi = reader.GetValue(4).ToString();
                gorev.ProfilResmi = reader.GetValue(3).ToString();
                gorev.AdımId = Convert.ToInt32(reader.GetValue(5)).ToString();
                gorev.GörevId = Convert.ToInt32(reader.GetValue(6)).ToString();

                try
                {
                    gorev.TarihZaman = Convert.ToDateTime(reader.GetValue(7)).ToString();
                }
                catch (Exception)
                { gorev.TarihZaman = ""; }
                gorev.GörevAdi = reader.GetValue(8).ToString();
                gorev.GörevResmi = reader.GetValue(9).ToString();
                gorev.AdimAdi = reader.GetValue(10).ToString();
                gorev.AdimResmi = reader.GetValue(11).ToString();
                gorev.Begenildimi = reader.GetValue(12).ToString();
                gorevlist.Add(gorev);

            }

            myConnection.Close();
            return gorevlist;
        }

        [Route("getGörevlerEtap1/GörevlerDirekt")]
        [HttpGet]
        public List<GorevEtap1Model> getGorevEtap1Model()
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GorevEtap1";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            GorevEtap1Model gorev = new GorevEtap1Model();
            List<GorevEtap1Model> gorevlist = new List<GorevEtap1Model>();
            while (reader.Read())
            {
                gorev = new GorevEtap1Model();

                gorev.GörevId = Convert.ToInt32(reader.GetValue(0)).ToString();
                gorev.GörevAdi = reader.GetValue(1).ToString();
                gorev.GörevResmi = reader.GetValue(2).ToString();
                gorevlist.Add(gorev);

            }

            myConnection.Close();
            return gorevlist;
        }

        [Route("api/Satinalma/getProfil/{userid}")]
        [HttpGet]
        public List<KullaniciProfil> getKullaniciprofil(int userid)
        {
            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd2.CommandText = "sp_BitirilenAdimSayisi_profil";
            sqlCmd2.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd2.Parameters["@userid"].Value = userid;
            sqlCmd2.Connection = myConnection;
            ///////
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_KullaniciProfil";
            sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd.Parameters["@userid"].Value = userid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();

            ///

            /////////////////////////////////////////////////////////////////////
            KullaniciProfil profil = new KullaniciProfil();
            List<KullaniciProfil> profillist = new List<KullaniciProfil>();
            while (reader.Read())
            {
                profil = new KullaniciProfil();
                profil.UserId = Convert.ToInt32(reader.GetValue(0)).ToString();
                profil.KullaniciAdi = reader.GetValue(1).ToString();
                profil.Okul = reader.GetValue(2).ToString();
                profil.GörevDerecesi = reader.GetValue(3).ToString();
                profil.ProfilYazısı = reader.GetValue(4).ToString();
                profil.ProfilResmi = reader.GetValue(5).ToString();
            }
            myConnection.Close();
            myConnection.Open();
            profil.bitengörevSayis = Convert.ToInt32(sqlCmd2.ExecuteScalar());
            profillist.Add(profil);

            myConnection.Close();
            return profillist;
        }

        [Route("getUserArkadaslarlistesi/{userid}")]
        [HttpGet]
        public List<arkadaslistesi> getArkadasListesi(int userid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_ArkadasListesi";
            sqlCmd.Parameters.Add("@myid", SqlDbType.Int);
            sqlCmd.Parameters["@myid"].Value = userid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            arkadaslistesi arkadas = new arkadaslistesi();
            List<arkadaslistesi> arkadaslist = new List<arkadaslistesi>();
            while (reader.Read())
            {
                arkadas = new arkadaslistesi();
                arkadas.UserId = Convert.ToInt32(reader.GetValue(0)).ToString();
                arkadas.KullaniciAdi = reader.GetValue(1).ToString();
                arkadas.ProfilResmi = reader.GetValue(2).ToString();
                arkadas.ArkUserId = Convert.ToInt32(reader.GetValue(3)).ToString();
                arkadaslist.Add(arkadas);
            }

            myConnection.Close();
            return arkadaslist;
        }

        [Route("getGörevAdımlariUser/{userid}/{gorevid}")]
        [HttpGet]
        public List<görevAdimlariUser> getGörevAdimlariUser(int userid, int gorevid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GörevAdimleri_User";
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int);
            sqlCmd.Parameters["@UserId"].Value = userid;
            sqlCmd.Parameters.Add("@GörevId", SqlDbType.Int);
            sqlCmd.Parameters["@GörevId"].Value = gorevid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            görevAdimlariUser görevU = new görevAdimlariUser();
            List<görevAdimlariUser> görevUlist = new List<görevAdimlariUser>();
            while (reader.Read())
            {
                görevU = new görevAdimlariUser();
                görevU.AdımId = Convert.ToInt32(reader.GetValue(0)).ToString();
                görevU.AdimAdi = reader.GetValue(1).ToString();
                görevU.AdimResmi = reader.GetValue(2).ToString();
                görevU.TamamlanmaDurumu = Convert.ToBoolean(reader.GetValue(3)).ToString();
                görevUlist.Add(görevU);
            }

            myConnection.Close();
            return görevUlist;
        }

        [Route("api/Satinalma/login")]
        [HttpPost]
        public HttpResponseMessage kullanicigiris([FromBody] KullaniciGiris m)
        {
            String json = "{\"UserId\":1,\"name\":\"a small object\" }";
            HttpResponseMessage responsetext = new HttpResponseMessage();
            myConnection.Open();
            SqlCommand sorgula = new SqlCommand("select UserId,email,Sifre,aktiflik from Login where email=@mail and Sifre=@sif and aktiflik = @aktf", myConnection);
            sorgula.Parameters.AddWithValue("@mail", m.email);
            sorgula.Parameters.AddWithValue("@sif", m.Sifre);
            sorgula.Parameters.AddWithValue("@aktf", Convert.ToBoolean(0));

            SqlDataReader dr = sorgula.ExecuteReader();

            if (dr.Read())
            {
                string myid = Convert.ToInt32(dr.GetValue(0)).ToString();

                json = "{\"UserId\":" + myid + ",\"name\":\"giris yapildi\" }";
            }
            else
            {

                json = "{\"UserId\":0,\"name\":\"Yanlis Kullanici Yada  Başka bir Cihazdan Daha Giris Yapılmış\" }";

            }

            myConnection.Close();

            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand("update Login  set aktiflik=@aktif where  email=@mail and Sifre=@sif", myConnection);
            sqlCmd.Parameters.AddWithValue("@mail", m.email);
            sqlCmd.Parameters.AddWithValue("@sif", m.email);
            sqlCmd.Parameters.AddWithValue("@aktif", Convert.ToBoolean(1));
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();


            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }


        [Route("api/postGörevtamamlamaAdim")]
        [HttpPost]
        public HttpResponseMessage Görevtamamlamaadimpost([FromBody] GörevtamamlamaPost x)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"Durum\": \"Başarili\" }";
            ///////////
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GorevTamamlamaAdimPost";
            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int);
            sqlCmd.Parameters["@UserId"].Value = x.UserId;
            sqlCmd.Parameters.Add("@GörevId", SqlDbType.Int);
            sqlCmd.Parameters["@GörevId"].Value = x.GörevId;
            sqlCmd.Parameters.Add("@AdımId", SqlDbType.Int);
            sqlCmd.Parameters["@AdımId"].Value = x.AdımId;
            sqlCmd.Parameters.Add("@TarihZaman", SqlDbType.DateTime);
            sqlCmd.Parameters["@TarihZaman"].Value = DateTime.Now;
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            ////////////////////
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/kayitislemleri/kayit")]
        [HttpPost]
        public HttpResponseMessage Kayitislemi([FromBody] Kayitislemi m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"a small object\" }";
            Random rastgele = new Random();
            int sayi = rastgele.Next();
            string FileName = "profilresmi" + "-" + Convert.ToInt32(sayi).ToString() + "Profil.png";
            try
            {                
                ResimCevir cevir = new ResimCevir();
                Bitmap ımage = new Bitmap(cevir.Base64Cevir(m.Okul));
                ımage.Save(HttpContext.Current.Server.MapPath("~/Resim/" + FileName), System.Drawing.Imaging.ImageFormat.Png);
                ımage.Dispose();

            }
            catch (Exception ex)
            {//
                json = "{\"UserId\":0,\"name\":\"" + ex.ToString() + "\" }";

            }
            try
            {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_Kayitislemi";
            sqlCmd.Parameters.Add("@email", SqlDbType.NVarChar, 50);
            sqlCmd.Parameters["@email"].Value = m.email;
            sqlCmd.Parameters.Add("@Sifre", SqlDbType.NVarChar, 50);
            sqlCmd.Parameters["@Sifre"].Value = m.Sifre;
            sqlCmd.Parameters.Add("@KullaniciAdi", SqlDbType.NVarChar, 50);
            sqlCmd.Parameters["@KullaniciAdi"].Value = m.KullaniciAdi;
            sqlCmd.Parameters.Add("@Okul", SqlDbType.NVarChar, 100);
            sqlCmd.Parameters["@Okul"].Value = FileName;
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"" +"Hata oluştu !!! "  + "\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }


        [Route("getGöreviBitirenleriGör/{Görevid}")]
        [HttpGet]
        public List<GöreviBitirenleriGör> getGöreviBitirenleriGör(int Görevid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GöreviBitirenleriGör";
            sqlCmd.Parameters.Add("@Görevid", SqlDbType.Int);
            sqlCmd.Parameters["@Görevid"].Value = Görevid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            GöreviBitirenleriGör görevU = new GöreviBitirenleriGör();
            List<GöreviBitirenleriGör> görevUlist = new List<GöreviBitirenleriGör>();
            while (reader.Read())
            {
                görevU = new GöreviBitirenleriGör();
                görevU.UserId = Convert.ToInt32(reader.GetValue(0)).ToString();
                görevU.KullaniciAdi = reader.GetValue(1).ToString();
                görevU.GörevDerecesi = reader.GetValue(2).ToString();
                görevU.ProfilResmi = reader.GetValue(3).ToString();
                görevUlist.Add(görevU);
            }

            myConnection.Close();
            return görevUlist;
        }
        [Route("getBildirimleriDöndür/{userid}")]
        [HttpGet]
        public List<BildirimleriDöndür> getBildirimleriDöndür(int userid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_BildirimleriDöndür";
            sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd.Parameters["@userid"].Value = userid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            BildirimleriDöndür model = new BildirimleriDöndür();
            List<BildirimleriDöndür> list = new List<BildirimleriDöndür>();
            while (reader.Read())
            {
                model = new BildirimleriDöndür();
                model.Myid = Convert.ToInt32(reader.GetValue(0)).ToString();
                model.BildirimiGöderenId = Convert.ToInt32(reader.GetValue(1)).ToString();
                model.TarihZaman = String.Format("{0:dd/MM/yy HH:mm}", reader.GetValue(2)).ToString();
                model.Yorum = Convert.ToBoolean(reader.GetValue(3)).ToString();
                model.GörevId = Convert.ToInt32(reader.GetValue(4)).ToString();
                model.AdimId = Convert.ToInt32(reader.GetValue(5)).ToString();
                model.bildirimeBakıldımı = Convert.ToBoolean(reader.GetValue(6)).ToString();
                model.begeni = Convert.ToBoolean(reader.GetValue(7)).ToString();
                model.ArkadasEkledin = Convert.ToBoolean(reader.GetValue(8)).ToString();
                model.ArkadaslıkİsteğiGeldi = Convert.ToBoolean(reader.GetValue(9)).ToString();
                model.MesajGeldi = Convert.ToBoolean(reader.GetValue(10)).ToString();
                model.KullaniciAdi = reader.GetValue(11).ToString();
                model.ProfilResmi = reader.GetValue(12).ToString();
                list.Add(model);
            }

            myConnection.Close();
            return list;
        }


        [Route("getArkadaslikistekleri/{userid}")]
        [HttpGet]
        public List<Arkadaslikistekleri> getArkadaslikistekleri(int userid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_ArkadaslikİstekleriGelenler";
            sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd.Parameters["@userid"].Value = userid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Arkadaslikistekleri model = new Arkadaslikistekleri();
            List<Arkadaslikistekleri> list = new List<Arkadaslikistekleri>();
            while (reader.Read())
            {
                model = new Arkadaslikistekleri();
                model.UserId = Convert.ToInt32(reader.GetValue(1)).ToString();
                model.KullaniciAdi = reader.GetValue(2).ToString();
                model.ProfilResmi = reader.GetValue(0).ToString();


                list.Add(model);
            }

            myConnection.Close();
            return list;
        }

        [Route("getBaskasınınProfilineGeç/{userid}/{istekGonderilenID}")]
        [HttpGet]
        public List<BaskasınınProfilineGeç> getBaskasınınProfilineGeç(int userid, int istekGonderilenID)
        {
            SqlCommand sqlCmd2 = new SqlCommand();
            sqlCmd2.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd2.CommandText = "sp_BitirilenAdimSayisi_profil";
            sqlCmd2.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd2.Parameters["@userid"].Value = istekGonderilenID;
            sqlCmd2.Connection = myConnection;
            ////
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_BaskasınınProfilineGeç";
            sqlCmd.Parameters.Add("@MyUserid", SqlDbType.Int);
            sqlCmd.Parameters["@MyUserid"].Value = userid;
            sqlCmd.Parameters.Add("@istekGönderilenID", SqlDbType.Int);
            sqlCmd.Parameters["@istekGönderilenID"].Value = istekGonderilenID;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            BaskasınınProfilineGeç model = new BaskasınınProfilineGeç();
            List<BaskasınınProfilineGeç> list = new List<BaskasınınProfilineGeç>();
            while (reader.Read())
            {
                model = new BaskasınınProfilineGeç();
                model.KullaniciAdi = reader.GetValue(0).ToString();
                model.Okul = reader.GetValue(1).ToString();
                model.GörevDerecesi = reader.GetValue(2).ToString();
                model.ProfilYazısı = reader.GetValue(3).ToString();
                model.ProfilResmi = reader.GetValue(4).ToString();
                try
                {
                    model.MyUserid = Convert.ToInt32(reader.GetValue(5)); /* null olup olmadıgının kontrolü :/ */
                }
                catch (Exception)
                {
                    model.MyUserid = Convert.ToInt32(0);
                }
                try
                {
                    model.istekGönderilenid = Convert.ToInt32(reader.GetValue(6));
                }
                catch (Exception)
                {
                    model.istekGönderilenid = Convert.ToInt32(0);
                }
                try
                {
                model.istekGönderilenid = Convert.ToInt32(reader.GetValue(7));
                }
                catch (Exception)
                {
                    model.istekGönderilenid = Convert.ToInt32(0);
                }

            }
            myConnection.Close();
            myConnection.Open();
            model.bitengörevSayis = Convert.ToInt32(sqlCmd2.ExecuteScalar());
            list.Add(model);

            myConnection.Close();
            
            return list;
        }
        [Route("getGörevTamamlamaPostYorumDısarıAcma/{userid}/{adimid}/{gorevid}")]
        [HttpGet]
        public List<GörevTamamlamaPostYorum_DısarıAcma> getGörevTamamlamaPostYorum_DısarıAcma(int userid, int adimid, int gorevid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_GörevTamamlamaPostYorum_DısarıAcma";
            sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
            sqlCmd.Parameters["@userid"].Value = userid;
            sqlCmd.Parameters.Add("@adimid", SqlDbType.Int);
            sqlCmd.Parameters["@adimid"].Value = adimid;
            sqlCmd.Parameters.Add("@görevid", SqlDbType.Int);
            sqlCmd.Parameters["@görevid"].Value = gorevid;
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            GörevTamamlamaPostYorum_DısarıAcma model = new GörevTamamlamaPostYorum_DısarıAcma();
            List<GörevTamamlamaPostYorum_DısarıAcma> list = new List<GörevTamamlamaPostYorum_DısarıAcma>();
            while (reader.Read())
            {
                model = new GörevTamamlamaPostYorum_DısarıAcma();
                model.YorumYapanId = Convert.ToInt32(reader.GetValue(0)).ToString();
                model.YorumAtilanTarih = String.Format("{0:dd/MM/yy HH:mm}", reader.GetValue(1)).ToString();
                model.YorumYapanAdi = reader.GetValue(2).ToString();
                model.Yorum = reader.GetValue(3).ToString();
                model.YorumyapanProfilResmi = reader.GetValue(4).ToString();
                list.Add(model);
            }

            myConnection.Close();
            return list;
        }

        [Route("api/sp_GörevTamamlamaPostYorum_iceAktarma")]
        [HttpPost]
        public HttpResponseMessage GörevTamamlamaPostYorum_iceAktarma([FromBody] GörevTamamlamaPostYorum_iceAktarma m)
        {


            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"Sonuc\":1,\"name\":\"Tamamlandı\" }";
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_GörevTamamlamaPostYorum_iceAktarma";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.userid);
                sqlCmd.Parameters.Add("@görevid", SqlDbType.Int);
                sqlCmd.Parameters["@görevid"].Value = Convert.ToInt32(m.görevid);
                sqlCmd.Parameters.Add("@adimid", SqlDbType.Int);
                sqlCmd.Parameters["@adimid"].Value = Convert.ToInt32(m.adimid);
                sqlCmd.Parameters.Add("@yorum", SqlDbType.NVarChar, 250);
                sqlCmd.Parameters["@yorum"].Value = m.yorum;
                sqlCmd.Parameters.Add("@yorumyapanid", SqlDbType.Int);
                sqlCmd.Parameters["@yorumyapanid"].Value = Convert.ToInt32(m.yorumyapanid);
                sqlCmd.Parameters.Add("@yorumyapanad", SqlDbType.NVarChar, 50);
                sqlCmd.Parameters["@yorumyapanad"].Value = m.yorumyapanad;
                sqlCmd.Parameters.Add("@yorumyapanProfilresmi", SqlDbType.NVarChar, 50);
                sqlCmd.Parameters["@yorumyapanProfilresmi"].Value = m.yorumyapanProfilresmi;
                sqlCmd.Parameters.Add("@yorumTarih", SqlDbType.DateTime);
                sqlCmd.Parameters["@yorumTarih"].Value = DateTime.Now;
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception ex)
            {
                json = "{\"Sonuc\":0,\"name\":" + ex + "}";

            }
            try
            {//userid, postun sahibinin id si
                SqlDataReader reader = null;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_RegidCek";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.userid);
                sqlCmd.Parameters.Add("@bildirimgonderenid", SqlDbType.Int);
                sqlCmd.Parameters["@bildirimgonderenid"].Value = Convert.ToInt32(m.yorumyapanid);
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    if (m.userid != m.yorumyapanid)
                    {
                        AndroidGCMPushNotification fcm = new AndroidGCMPushNotification();
                        fcm.SendNotificationJson(reader.GetValue(0).ToString(), "Bir gönderinize yorum yaptı.", reader.GetValue(1).ToString());
                        //fcm fonksiyonuna Kullanıcı adını baslık olarak gönderdim
                        //mesaj kısmına bildirim atan kişinin ne bildirimi atılacagı yazılcak
                        // resimde notification çektiğinde gözükücek
                        // mesaj Kısmını Düzelt Burdan düzenli gidicek -- (düzelttikte sonra bu yorumu sil)
                    }
                    break;
                }
            }
            catch (Exception)
            {
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
            // Yorum Geldiği zaman, o posta yorum bırakan kişelerin telefonuna bildirim gidicek 
            // Ve  post Sahibine
            // userid üzerinden FCM regid çekilmesi lazım 
            // Regid üzerinde Pushnitification fonk. çağrılcak

        }
        [Route("api/RegID/KayitAl")]
        [HttpPost]
        public HttpResponseMessage Regid([FromBody] RegidKaydet m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_RegIdKaydet";
                sqlCmd.Parameters.Add("@Regid", SqlDbType.NVarChar, -1);
                sqlCmd.Parameters["@Regid"].Value = m.Regid;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = m.UserId;
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception)
            {

                json = "{\"UserId\":1,\"name\":\"Hata oluştu\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/ArkadaslikIStekleri/Onayla")]
        [HttpPost]
        public HttpResponseMessage onayla([FromBody] ArkadaslikiStekleriOnayla m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_ArkadaslikisteklerOnayla";
                sqlCmd.Parameters.Add("@Arkuserid", SqlDbType.Int);
                sqlCmd.Parameters["@Arkuserid"].Value = Convert.ToInt32(m.Arkuserid);
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.UserId);
                sqlCmd.Parameters.Add("@zaman", SqlDbType.DateTime);
                sqlCmd.Parameters["@zaman"].Value = Convert.ToDateTime(DateTime.Now);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception)
            {
                json = "{\"UserId\":1,\"name\":\"Hata oluştu\" }";
            }
            try
            {//userid, postun sahibinin id si
                SqlDataReader reader = null;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_RegidCek";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.UserId);
                sqlCmd.Parameters.Add("@bildirimgonderenid", SqlDbType.Int);
                sqlCmd.Parameters["@bildirimgonderenid"].Value = Convert.ToInt32(m.Arkuserid);
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    if (m.UserId != m.Arkuserid)
                    {
                        AndroidGCMPushNotification fcm = new AndroidGCMPushNotification();
                        fcm.SendNotificationJson(reader.GetValue(0).ToString(), "Arkadaşlık isteğinizi onayladı", reader.GetValue(1).ToString());
                    }
                    break;
                }
            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Gönderimde Hata oluştu\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/ArkadaslikIStekleri/REd")]
        [HttpPost]
        public HttpResponseMessage red([FromBody] ArkadaslikStekleriRed m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_ArkadaslikiStekleriRed";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.userid);
                sqlCmd.Parameters.Add("@reduserid", SqlDbType.Int);
                sqlCmd.Parameters["@reduserid"].Value = Convert.ToInt32(m.reduserid);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception)
            {

                json = "{\"UserId\":1,\"name\":\"Hata oluştu\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/Postbegeni")]
        [HttpPost]
        public HttpResponseMessage Postbegeni([FromBody] postbegeni m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_BegeniPost";
                sqlCmd.Parameters.Add("@Myuserid", SqlDbType.Int);
                sqlCmd.Parameters["@Myuserid"].Value = Convert.ToInt32(m.MyUserId);
                sqlCmd.Parameters.Add("@gönderenid", SqlDbType.Int);
                sqlCmd.Parameters["@gönderenid"].Value = Convert.ToInt32(m.BildirimGönderenId);
                sqlCmd.Parameters.Add("@görevid", SqlDbType.Int);
                sqlCmd.Parameters["@görevid"].Value = Convert.ToInt32(m.GörevId);
                sqlCmd.Parameters.Add("@adimid", SqlDbType.Int);
                sqlCmd.Parameters["@adimid"].Value = Convert.ToInt32(m.AdimId);
                sqlCmd.Parameters.Add("@tarih", SqlDbType.DateTime);
                sqlCmd.Parameters["@tarih"].Value = DateTime.Now;
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception)
            {
                json = "{\"UserId\":1,\"name\":\"Gönderimde Hata oluştu\" }";
            }
            try
            {//userid, postun sahibinin id si
                SqlDataReader reader = null;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_RegidCek";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.MyUserId);
                sqlCmd.Parameters.Add("@bildirimgonderenid", SqlDbType.Int);
                sqlCmd.Parameters["@bildirimgonderenid"].Value = Convert.ToInt32(m.BildirimGönderenId);
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    if (m.MyUserId != m.BildirimGönderenId)
                    {
                        AndroidGCMPushNotification fcm = new AndroidGCMPushNotification();
                        fcm.SendNotificationJson(reader.GetValue(0).ToString(), "Bir gönderinizi beğendi", reader.GetValue(1).ToString());
                        //fcm fonksiyonuna Kullanıcı adını baslık olarak gönderdim
                        //mesaj kısmına bildirim atan kişinin ne bildirimi atılacagı yazılcak
                        // resimde notification çektiğinde gözükücek
                        // mesaj Kısmını Düzelt Burdan düzenli gidicek -- (düzelttikte sonra bu yorumu sil)
                    }
                    break;
                }
            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Gönderimde Hata oluştu\" }";
            }


            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/PostbegeniGerial")]
        [HttpPost]
        public HttpResponseMessage PostbegeniGeriAl([FromBody] postBegeniGeriAl m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_BegeniGeriAl";
                sqlCmd.Parameters.Add("@Myuserid", SqlDbType.Int);
                sqlCmd.Parameters["@Myuserid"].Value = Convert.ToInt32(m.MyUserId);
                sqlCmd.Parameters.Add("@gönderenid", SqlDbType.Int);
                sqlCmd.Parameters["@gönderenid"].Value = Convert.ToInt32(m.BildirimGönderenId);
                sqlCmd.Parameters.Add("@görevid", SqlDbType.Int);
                sqlCmd.Parameters["@görevid"].Value = Convert.ToInt32(m.GörevId);
                sqlCmd.Parameters.Add("@adimid", SqlDbType.Int);
                sqlCmd.Parameters["@adimid"].Value = Convert.ToInt32(m.AdimId);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Gönderimde Hata oluştu\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/PostArkadasEkle")]
        [HttpPost]
        public HttpResponseMessage ArkadasEkle([FromBody] ArkadasEkle m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_ArkadasEkle";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.UserId);
                sqlCmd.Parameters.Add("@istekgönderen", SqlDbType.Int);
                sqlCmd.Parameters["@istekgönderen"].Value = Convert.ToInt32(m.gönderilenuserid);
                sqlCmd.Parameters.Add("@zaman", SqlDbType.DateTime);
                sqlCmd.Parameters["@zaman"].Value = Convert.ToDateTime(DateTime.Now);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();
                try
                {//userid, postun sahibinin id si
                    SqlDataReader reader = null;
                    SqlCommand sqlCmd5 = new SqlCommand();
                    sqlCmd5.Connection = myConnection;
                    myConnection.Open();
                    sqlCmd5.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd5.CommandText = "sp_RegidCek";
                    sqlCmd5.Parameters.Add("@userid", SqlDbType.Int);
                    sqlCmd5.Parameters["@userid"].Value = Convert.ToInt32(m.UserId);
                    sqlCmd5.Parameters.Add("@bildirimgonderenid", SqlDbType.Int);
                    sqlCmd5.Parameters["@bildirimgonderenid"].Value = Convert.ToInt32(m.gönderilenuserid);
                 
                    reader = sqlCmd5.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        if (m.UserId != m.gönderilenuserid)
                        {
                            AndroidGCMPushNotification fcm = new AndroidGCMPushNotification();
                            fcm.SendNotificationJson(reader.GetValue(0).ToString(), "Size arkadaşlık isteği yolladı !", reader.GetValue(1).ToString());                           
                        }
                        break;
                    }
                }
                catch (Exception)
                {
                    json = "{\"UserId\":1,\"name\":\"Gönderimde Hata oluştu\" }";
                }

            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Gönderimde Hata oluştu !!!\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/PostArkadaslıktanCıkar")]
        [HttpPost]
        public HttpResponseMessage ArkadasCıkar([FromBody] ArkadaslardanCıkar m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"Arkadaşlıktan Çıkarıldı.\" }";
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_ArkadaşlıktanCıkar";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.UserId);
                sqlCmd.Parameters.Add("@istekgönderen", SqlDbType.Int);
                sqlCmd.Parameters["@istekgönderen"].Value = Convert.ToInt32(m.gönderilenuserid);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Hata oluştu !!!\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }
        [Route("api/BegeniListesiDodur_FragA_Yorumlar/{adimid}/{gorevid}/{gonderiuserid}")]
        [HttpGet]
        public List<BegeniListesiDodur_FragA_Yorumlar> BegeniListesiDodur_FragA_Yorumlar(int adimid, int gorevid, int gonderiuserid)
        {

            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_BegeniListesiDodur_FragA_Yorumlar";
            sqlCmd.Parameters.Add("@myuserid", SqlDbType.Int);
            sqlCmd.Parameters["@myuserid"].Value = Convert.ToInt32(gonderiuserid);
            sqlCmd.Parameters.Add("@görevid", SqlDbType.Int);
            sqlCmd.Parameters["@görevid"].Value = Convert.ToInt32(gorevid);
            sqlCmd.Parameters.Add("@adimid", SqlDbType.Int);
            sqlCmd.Parameters["@adimid"].Value = Convert.ToInt32(adimid);
            reader = sqlCmd.ExecuteReader();
            BegeniListesiDodur_FragA_Yorumlar model = new BegeniListesiDodur_FragA_Yorumlar();
            List<BegeniListesiDodur_FragA_Yorumlar> list = new List<BegeniListesiDodur_FragA_Yorumlar>();
            while (reader.Read())
            {
                model = new BegeniListesiDodur_FragA_Yorumlar();
                model.UserId = Convert.ToInt32(reader.GetValue(0)).ToString();
                model.KullaniciAdi = reader.GetValue(1).ToString();
                model.ProfilResmi = reader.GetValue(2).ToString();
                list.Add(model);
            }


            myConnection.Close();
            return list;
        }

        [Route("api/profilGüncelle")]
        [HttpPost]
        public HttpResponseMessage profilGüncelle([FromBody] profilgüncellepost m)
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next();
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"başarili\" }";
            //encode
            //String zaman = 
            string FileName = Convert.ToInt32(m.userid).ToString()+"-"+ Convert.ToInt32(sayi).ToString()+"Profil.png";

            try
            {
                ResimCevir cevir = new ResimCevir();
                
               
               Bitmap ımage = new Bitmap(cevir.Base64Cevir(m.resimyolu));             
               ımage.Save(HttpContext.Current.Server.MapPath("~/Resim/" + FileName), System.Drawing.Imaging.ImageFormat.Png);

                ımage.Dispose();

            }
            catch (Exception ex )
            {//
                json = "{\"UserId\":0,\"name\":\""+ex.ToString()+"\" }";

            }
            

            try
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_profilDüzenle";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.userid);

                sqlCmd.Parameters.Add("@email", SqlDbType.NVarChar, 50);
                sqlCmd.Parameters["@email"].Value = m.email;

                sqlCmd.Parameters.Add("@sifre", SqlDbType.NVarChar, 50);
                sqlCmd.Parameters["@sifre"].Value = m.Sifre;

                sqlCmd.Parameters.Add("@kullaniciadi", SqlDbType.NVarChar, 50);
                sqlCmd.Parameters["@kullaniciadi"].Value = m.KullaniciAdi;

                sqlCmd.Parameters.Add("@profilyazısı", SqlDbType.NVarChar, 150);
                sqlCmd.Parameters["@profilyazısı"].Value = m.profilyazısı;

                sqlCmd.Parameters.Add("@resimyolu", SqlDbType.NVarChar, 150);
                sqlCmd.Parameters["@resimyolu"].Value = FileName;


                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception)
            {
                json = "{\"UserId\":1,\"name\":\"Gönderimde Hata oluştu\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;

        }

        [Route("api/MesajkutusuListele/{userid}")]
        [HttpGet]
        public List<MesajKutusu> MesajkutusuListele(int userid)
        {
            SqlDataReader reader = null;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_MesajKutusunuListele";
            sqlCmd.Parameters.Add("@Myuserid", SqlDbType.Int);
            sqlCmd.Parameters["@Myuserid"].Value = Convert.ToInt32(userid);
            reader = sqlCmd.ExecuteReader();
            MesajKutusu model = new MesajKutusu();
            List<MesajKutusu> list = new List<MesajKutusu>();
            while (reader.Read())
            {
                model = new MesajKutusu();
                model.MyUserid = Convert.ToInt32(reader.GetValue(0)).ToString();
                model.mesajgelenUserid = Convert.ToInt32(reader.GetValue(1)).ToString();
                model.SonMesaj = reader.GetValue(2).ToString();
                model.SonMesajZamanı = reader.GetValue(3).ToString();
                model.ProfilResmi = reader.GetValue(4).ToString();
                model.KullaniciAdi = reader.GetValue(5).ToString();
                list.Add(model);
            }


            myConnection.Close();
            return list;
        }

        [Route("api/MesajKutusu/Gonder")]
        [HttpPost]
        public HttpResponseMessage MesajKutusuGonder([FromBody] MesajGonder m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":\""+ String.Format("{0:dd/MM/yy HH:mm}", DateTime.Now).ToString() + "\",\"name\":\"Mesaj Gönderildi\" }";
            try
            {
                SqlDataReader reader = null;
                SqlCommand sqlCmd5 = new SqlCommand();
                sqlCmd5.Connection = myConnection;
                myConnection.Open();
                sqlCmd5.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd5.CommandText = "sp_RegidCek_MesajGonder";
                sqlCmd5.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd5.Parameters["@userid"].Value = Convert.ToInt32(m.Myuserid); // gidenkişinin id si
                sqlCmd5.Parameters.Add("@bildirimgonderenid", SqlDbType.Int);
                sqlCmd5.Parameters["@bildirimgonderenid"].Value = Convert.ToInt32(m.gönderilenUserid);
                sqlCmd5.Parameters.Add("@TariZaman", SqlDbType.DateTime);
                sqlCmd5.Parameters["@TariZaman"].Value = Convert.ToDateTime(DateTime.Now);
                sqlCmd5.Parameters.Add("@Mesaj", SqlDbType.NVarChar,500);
                sqlCmd5.Parameters["@Mesaj"].Value = m.Mesaj;
                reader = sqlCmd5.ExecuteReader();
                while (reader.Read())
                {
                    if (m.Myuserid != m.gönderilenUserid)
                    {                 
                        AndroidGCMPushNotification2 fcm = new AndroidGCMPushNotification2();
                        fcm.SendNotificationJson(reader.GetValue(0).ToString(),m.Mesaj , reader.GetValue(1).ToString()+","+ String.Format("{0:dd/MM/yy HH:mm}", DateTime.Now).ToString()+","+ Convert.ToInt32(m.gönderilenUserid).ToString());
                    }
                    break;
                }
            }
            catch (Exception ex )
            {
                json = "{\"UserId\":\""+ ex.ToString() + "\",\"name\":\"Hata oluştu !!!\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }

        [Route("api/MesajKutusuMesajlarıSil")]
        [HttpPost]
        public HttpResponseMessage MesajKutusuMesajlarıSil([FromBody] MesajKutusu_MesajlarıSil m)
        {
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"UserId\":1,\"name\":\"Silindi\" }";
            try
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_MesajKutusu_MesajlarıSil";
                sqlCmd.Parameters.Add("@Myuserid", SqlDbType.Int);
                sqlCmd.Parameters["@Myuserid"].Value = Convert.ToInt32(m.UserId);
                sqlCmd.Parameters.Add("@mesajgelenUserid", SqlDbType.Int);
                sqlCmd.Parameters["@mesajgelenUserid"].Value = Convert.ToInt32(m.mesajgelenUserid);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception)
            {
                json = "{\"UserId\":0,\"name\":\"Hata oluştu !!!\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }

        [Route("api/CikisYap_Temizle")]
        [HttpPost]
        public HttpResponseMessage CikisYap_Temizle([FromBody] CikisYap m)
        {
            string regid ="";
            HttpResponseMessage responsetext = new HttpResponseMessage();
            String json = "{\"regid\":1,\"name\":\"Çıkış Yapıldı\" }";
            try
            {
                
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "sp_CikisYap";
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int);
                sqlCmd.Parameters["@userid"].Value = Convert.ToInt32(m.userid);
               // sqlCmd.ExecuteNonQuery();
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.Read())
                {
                     regid = reader.GetValue(0).ToString();
                }
                json = "{\"regid\":\""+ regid + "\",\"name\":\"Çıkış Yapıldı\" }";
                myConnection.Close();
            }
            catch (Exception)
            {
                json = "{\"regid\":0,\"name\":\"Hata oluştu !!!\" }";
            }
            responsetext.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return responsetext;
        }

    }


    public class ResimCevir
    {
        public Image Base64Cevir(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0,imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
    }


    public class AndroidGCMPushNotification // servırıma düşen ARk. istekleri ,
    {
        public class Notification
        {
            public string title { get; set; }
            public string text { get; set; }
        }

        public class Example
        {
            public string to { get; set; }
            public Notification notification { get; set; }
        }

        public string SendNotificationJson(string id, string msg,string baslik)
        { // apiler de sqle kayıt işlemi tamamlandıktan sonra, bu fonskiyon çağrılarak FCM ye bildirim atılır eş zamanlı
            // olarak telefonlara düşer 

            var AuthString = "AAAAWUIPt74:APA91bEFgfe-FBWJAojFXOvwgUWvatWkHscgt3_YchvoAByWal-CX1--c0A4ob9rwBvAcseY5-36JqR7kSkXaXywZp_xVrgc7fx98EIvKq5Aep4_DsP8wjEDRCphHLfuElSLAOcsMjJEmfmkVZT5MhMUaaujzJvcsQ";/*İlgili açıklama aşağıda yapılmıştır.*/
            var RegistrationID = id;
            var Message = msg;
            //-- Firebase Cloud Message HttpWebRequest oluşturuyoruz. --//
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;

            Example ex = new Example();
            ex.to = id;
            Notification note = new Notification { title = baslik, text = msg };
            ex.notification = note;
            string postData = JsonConvert.SerializeObject(ex);/*FCM için json objesine dönüşüm yapıyoruz.*/
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);/*byte dizisi haline getiriyoruz*/
            Request.ContentType = "application/json;charset=UTF-8";/*Request content tipini json olarak belirtiyoruz.*/
            Request.ContentLength = byteArray.Length;
            Request.Headers.Add(string.Format("Authorization:key={0}", AuthString));
            //-- Delegate kullanarak Server sertifikasını onayladık --//
            ServicePointManager.ServerCertificateValidationCallback += delegate (
            object
            sender,
            System.Security.Cryptography.X509Certificates.X509Certificate
            pCertificate,
            System.Security.Cryptography.X509Certificates.X509Chain pChain,
            System.Net.Security.SslPolicyErrors pSSLPolicyErrors)
            {
                return true;
            };
            //-- Stream nesnesi oluşturduk ve byte dizisini yazdırdık. --// 
            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //-- Mesajı gönderdik --//
            WebResponse Response = Request.GetResponse();
            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;/*Gelen mesajı kodunu aldık.*/
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                return "Unauthorized - need new token";/*Token FCm tarafından devre dışı kalmıştır.*/

            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                return "Response from web service isn't OK";/*Mesaj onaylanmamıştır.*/
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadLine();
            string result = Reader.ReadToEnd();
            Reader.Close();
            return result;
        }

    }

    public class AndroidGCMPushNotification2 // servırıma düşen ARk. istekleri ,
    {
        public class Notification
        {
            public string title { get; set; }
            public string text { get; set; }
        }
        public class Data
        {
            public string baslikk { get; set; }
            public string mesaj { get; set; }
        }
        public class Example
        {
            public string to { get; set; }
            public Notification notification { get; set; }
            public Data data { get; set; }
        }

        public string SendNotificationJson(string id, string msg, string baslik)
        { // apiler de sqle kayıt işlemi tamamlandıktan sonra, bu fonskiyon çağrılarak FCM ye bildirim atılır eş zamanlı
            // olarak telefonlara düşer 

            var AuthString = "AAAAWUIPt74:APA91bEFgfe-FBWJAojFXOvwgUWvatWkHscgt3_YchvoAByWal-CX1--c0A4ob9rwBvAcseY5-36JqR7kSkXaXywZp_xVrgc7fx98EIvKq5Aep4_DsP8wjEDRCphHLfuElSLAOcsMjJEmfmkVZT5MhMUaaujzJvcsQ";/*İlgili açıklama aşağıda yapılmıştır.*/
            var RegistrationID = id;
            var Message = msg;
            //-- Firebase Cloud Message HttpWebRequest oluşturuyoruz. --//
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;

            Example ex = new Example();
            ex.to = id;
  
        
            Data data1 = new Data { baslikk = baslik, mesaj = msg };
            ex.data = data1;
            string postData = JsonConvert.SerializeObject(ex);/*FCM için json objesine dönüşüm yapıyoruz.*/
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);/*byte dizisi haline getiriyoruz*/
            Request.ContentType = "application/json;charset=UTF-8";/*Request content tipini json olarak belirtiyoruz.*/
            Request.ContentLength = byteArray.Length;
            Request.Headers.Add(string.Format("Authorization:key={0}", AuthString));
            //-- Delegate kullanarak Server sertifikasını onayladık --//
            ServicePointManager.ServerCertificateValidationCallback += delegate (
            object
            sender,
            System.Security.Cryptography.X509Certificates.X509Certificate
            pCertificate,
            System.Security.Cryptography.X509Certificates.X509Chain pChain,
            System.Net.Security.SslPolicyErrors pSSLPolicyErrors)
            {
                return true;
            };
            //-- Stream nesnesi oluşturduk ve byte dizisini yazdırdık. --// 
            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //-- Mesajı gönderdik --//
            WebResponse Response = Request.GetResponse();
            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;/*Gelen mesajı kodunu aldık.*/
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                return "Unauthorized - need new token";/*Token FCm tarafından devre dışı kalmıştır.*/

            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                return "Response from web service isn't OK";/*Mesaj onaylanmamıştır.*/
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadLine();
            string result = Reader.ReadToEnd();
            Reader.Close();
            return result;
        }

    }


}
