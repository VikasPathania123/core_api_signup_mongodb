namespace Demo.API.Mapper
{
    using Demo.API.Mapper.MappingProfiles;
    using AutoMapper;

    /// <summary>
    /// Mapper init.
    /// </summary>
    public class MapperInit
    {
        public static MapperConfiguration MappInit()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserAutoMapperProfile());
                cfg.ValidateInlineMaps = false;
            });
        }
    }
}
