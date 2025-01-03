namespace FinalProject.Models.Repositories
{
    public static class Repository
    {
        public static List<User> Kullanici_Bilgileri = new List<User>();
        public static void Ekle(User model)
        {
            Kullanici_Bilgileri.Add(model);
        }
    }
}
