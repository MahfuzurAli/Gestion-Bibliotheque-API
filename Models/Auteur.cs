namespace GestionBibliothequeAPI.Models
{
    public class Auteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }

        public void DateNaissanceUTC(DateTime date)
        {
            DateNaissance = date.ToUniversalTime();
        }
    }
}
