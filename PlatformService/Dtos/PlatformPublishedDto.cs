using PlatformService.Enum;

namespace PlatformService.Dtos
{
    public class PlatformPublishedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EventType Event { get; set; }
    }
}
