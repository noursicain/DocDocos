﻿using System;
using System.Collections.Generic;
using System.Text;




namespace DocDocos
{
    /// <summary>
    /// Contient le squelette
    /// du HTML 
    /// </summary>
    public  class HTMLHelper
    {
        private const  string Guid =
            "!/$%svvhvbfhvbhk???hdhdhdhdhFFJH";

        /// <summary>
        /// Liste de toutes les bariables de 
        /// substitution utilisées
        /// </summary>
        /// <returns></returns>
        internal  static List<string> Variables()
        {
             List<string> Resultat =
                   new List<string>();
            Resultat.Add("{{ContenuLigne}}");
            Resultat.Add("{{ContenuCellule}}");
            Resultat.Add("{{Rangee}}");
            Resultat.Add("{{Summary}}");
            Resultat.Add("{{Return}}");
            Resultat.Add("{{RangeeParms}}");

            return Resultat; 
        }


        /// <summary>
        /// Substitue des variables
        /// du html par les valeurs
        /// en replaçant la balise de variable
        /// pour une autre occurence
        /// </summary>
        /// <param name="HTNLGlobal">HTML qui contient les variables</param>
        /// <param name="IdentifiantVariable">Identifiant de la variables comme par exemple {{Rangee}}</param>
        /// <param name="ContenuVariables">Valeur de la variable avec le HTML déjà encodé</param>
        /// <returns>HTML avec les variables substituée par leur valeurs</returns>
        internal  string ConstruireHTML(
            string HTNLGlobal,
            string IdentifiantVariable,
            string ContenuVariables)
        {
            String Resultat = "";

            if(IdentifiantVariable.Contains ("{{"))
            {
                Resultat =
                    HTNLGlobal.Replace(
                    IdentifiantVariable,
                   ContenuVariables + Guid);
                Resultat = Resultat .Replace(
                Guid,
                IdentifiantVariable);
                return Resultat; 
            }
            else
                throw new Exception(
                    "Lavariable " +
                    IdentifiantVariable +
                    " doit être délimitée par {{ et }}"); 
        }

        /// <summary>
        /// Enlève les identifiants de variables
        /// restants dans le HTML
        /// </summary>
        /// <returns></returns>
        internal  string  MenageHTNL(
            string HTML)
        {
            string  Resultat = HTML.Replace(GabaritSummary(),"") ;
            foreach(string IDVariable in Variables())
            {
                Resultat = Resultat.Replace(
                    IDVariable,"" );   
            }
            return Resultat;
        }

        /// <summary>
        /// Retourne le gabarit affichant
        /// l'infomration dans lapage.
        /// </summary>
        /// <returns></returns>
        internal  string GabaritInterne()
        {
            return 
                GabaritSummary() +
                GabaritParms() +
                GabaritReturn() + 
                GabaritTableau();
        }
        
        internal string GabaritReturn()
        {
            return "<h2>Retourne</h2><p>{{Return}}</p>";

        }

        internal string GabaritParms()
        {
            return "<h2>Paramètres</h2>" +
                "<table border=\"1\">{{RangeeParms}}</table>";

        }


        internal string  AjouterRetour(
            string HTML,
            string Retour)
        {
            if (string.IsNullOrEmpty(Retour))
                HTML = HTML.Replace(
                    GabaritReturn(), "");
            else
                HTML = ConstruireHTML(HTML,
                    "{{Return}}",
                    EncodeHTML(Retour));

            return HTML;
        }

        /// <summary>
        /// Crée un hiperlien
        /// en html
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Nom"></param>
        /// <returns></returns>
        internal  string ConstruireLien(
            string URL,
            string Nom)
        {
            return "<a href=\"" + URL + "\">" +
                EncodeHTML(Nom) + "</a>";
        }

    /// <summary>
    /// Retourne le gabarit pour
    /// le traitement d'une ligne de tableau
    /// </summary>
    /// <returns></returns>
    private  string GabaritRangee()
        {
            return "<tr>{{ContenuLigne}}" +
                Environment.NewLine   + "</tr>";
        }

        private  string GabaritCelluleTable()
        {
            return Environment.NewLine + "   " +
                "<td>{{ContenuCellule}}</td>";
        }

        private  String GabaritSummary()
        {
            return "<h2>Sommaire</h2><p>{{Summary}}</p>";
        }

        /// <summary>
        /// Retourne le html définissant une
        /// table HTML avec une zone pour
        /// insérer l'information du tableau
        /// </summary>
        /// <returns>HTML représentant le début et la fin d'une table html</returns>
        private  string GabaritTableau()
        {
            return Environment.NewLine +
                "<table border=\"1\">{{Rangee}}</table>" +
                Environment.NewLine;  
        }

        /// <summary>
        /// Retourne un gabarit de démonstration
        /// utilisé au casoù le fichier de 
        /// gabatir serait introuvable
        /// </summary>
        /// <returns></returns>
        public static string GabaritDemo()
        {
            string Resultat = "";
            Resultat = "<!DOCTYPE html>" + Environment.NewLine;
            Resultat += "<html><head>" + Environment.NewLine  ;
            Resultat += "<meta charset=\"utf-8\" />" + Environment.NewLine;
            Resultat += "<title>{{Titre}}</title>" + Environment.NewLine;
            Resultat += "</head><body>" + Environment.NewLine;
            Resultat += "<h1>{{Titre}}</h1>" + Environment.NewLine;
            Resultat += "<p>{{Contenu}}</p></body></html>" + Environment.NewLine;


            return Resultat; 


        }

        /// <summary>
        /// Encode en équivalent html les caracttères
        /// probématiques dans l'affichage d'un document html
        /// </summary>
        /// <param name="HTNL"></param>
        /// <returns></returns>
        internal  String EncodeHTML(
            string HTNL)
        {
            HTNL = HTNL.Replace("<", "&lt;");
            HTNL = HTNL.Replace(">", "&gt;");
            HTNL = HTNL.Replace("&", "&amp;");
            HTNL = HTNL.Replace("{", "&#123;");
            HTNL = HTNL.Replace("}", "&#125;");


            return HTNL;

        }

        /// <summary>
        /// Crée un bloc de HTML
        /// représentant une rangée de données
        /// d'un tableau
        /// </summary>
        /// <param name="Cellule1">Valeur de la première cullule de la rangée. Elle doit être encodée en HTML si besoin est.</param>
        /// <param name="Cellule2"></param>
        /// <param name="Cellule2"></param>
        /// <returns>HTML créé</returns>
        internal   string CreerRangee(
            string Cellule1,
            string Cellule2 = null,
            string Cellule3 = null)
        {
            string Resultat = 
                GabaritCelluleTable().Replace(
                    "{{ContenuCellule}}",
                    Cellule1);

            if(Cellule2 != null)
                Resultat +=
                  GabaritCelluleTable().Replace(
                    "{{ContenuCellule}}",
                    Cellule2);

            if (!string.IsNullOrEmpty(Cellule3))
                Resultat +=
                  GabaritCelluleTable().Replace(
                    "{{ContenuCellule}}",
                    Cellule3);
            return Environment.NewLine + 
                GabaritRangee().Replace(
                "{{ContenuLigne}}", Resultat); 
        } // methode

        internal string AjouterParms(
                    string HTML,
                    Dictionary<string , string > DictParam )
        {
            if (DictParam.Count == 0)
                HTML = HTML.Replace(GabaritParms(),"");
            else
            {
                String RangeesParms = "";

                foreach(var ListeParm in
                    DictParam.Keys )
                {
                    RangeesParms +=
                        CreerRangee(
                            EncodeHTML( ListeParm),
                            EncodeHTML(DictParam[ListeParm].ToString()) );
                }
                HTML = ConstruireHTML(
            HTML,
            "{{RangeeParms}}",
            RangeesParms);



            }
            return HTML; 
        }

        internal string AjouterSommmaire(
            string HTMLGlobal,
            string Sommaire)
        {
            if (string.IsNullOrEmpty(Sommaire))
                return HTMLGlobal;
            else
            {
                return ConstruireHTML(HTMLGlobal , 
                    "{{Summary}}",
                    Sommaire);
            }
        }


    } //class
}
