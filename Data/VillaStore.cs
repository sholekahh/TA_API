using percobaan_mod9_kel04.Models.NewFolder1;

namespace percobaan_mod9_kel04.NewFolder
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1, Name="Villa Tembalang", Occupancy=1, Sqft=300 },
            new VillaDTO{Id=2,Name="Villa Ungaran", Occupancy=3, Sqft=150}
        };
    } 
}
