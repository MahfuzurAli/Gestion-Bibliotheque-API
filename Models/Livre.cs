namespace GestionBibliothequeAPI.Models
{
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string ISBN { get; set; }
        public DateTime DatePublication { get; set; }
        public int AuteurId { get; set; }
        public Auteur Auteur { get; set; }
    }
}
