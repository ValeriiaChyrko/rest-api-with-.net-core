using api_sample_valla.Models.Dto;

namespace api_sample_valla.Data;

public class VillaStore
{
    public static List<VillaDto> villasList = new List<VillaDto>
    {
        new VillaDto{Id=1, Name="Pool View"},
        new VillaDto{Id=2, Name="Beach View"}
    };
}