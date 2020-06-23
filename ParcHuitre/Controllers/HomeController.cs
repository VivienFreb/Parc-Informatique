using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace ParcHuitre.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string connectionString = "Server = localhost; Database = parcinfo; Uid = root; Pwd =''";

            MySqlConnection connect = new MySqlConnection(connectionString);

            connect.Open();

            MySqlCommand CommandSecteur = new MySqlCommand("SELECT IdMateriel, NomMateriel, MarqueFournisseur, ReferenceMateriel, materiel.idService, NomService,"
            + "DateHAMateriel, DureeGarantieMateriel, DateFinGarantieMateriel, AChanger FROM `materiel`" 
            + "INNER JOIN fournisseur ON (fournisseur.ReferenceFournisseur = materiel.ReferenceMateriel) INNER JOIN service ON (service.idService = materiel.idService)", connect);

            MySqlDataReader Reader = CommandSecteur.ExecuteReader();

            string materiel = "";
            while (Reader.Read())
            {
                string verif = Reader["AChanger"].ToString();
                string achanger ="";
                TimeSpan dif = Convert.ToDateTime(Reader["DateFinGarantieMateriel"]) - DateTime.Now;
                int nbrjourgarantie = dif.Days;
                if(verif.ToString() == "True")
                {
                    achanger = "<br><strong style = 'color : red;'>Le matériel doit être changé car sa garantie se termine dans " + nbrjourgarantie + " jours</strong></br>";
                }
                materiel += "<tr><td>" + Reader["IdMateriel"] + "</td><td> " + Reader["NomMateriel"] + achanger + "</td><td> " + Reader["MarqueFournisseur"] + "</td><td>" +
                    Reader["ReferenceMateriel"] + "</td><td>" + Reader["idService"] + "</td><td>" + Reader["NomService"] + "</td><td>" + Convert.ToDateTime(Reader["DateHAMateriel"]).ToShortDateString() + "</td><td>" + Reader["DureeGarantieMateriel"] + "</td><td>" + Convert.ToDateTime(Reader["DateFinGarantieMateriel"]).ToShortDateString() + "</td></tr>";
                achanger = "";
            }
            
            ViewData["resultat"] = materiel;

            return View("Index");
        }
    }
}