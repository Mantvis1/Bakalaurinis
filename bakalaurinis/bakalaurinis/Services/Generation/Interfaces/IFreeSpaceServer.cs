using bakalaurinis.Dtos;
using System.Collections.Generic;

namespace bakalaurinis.Services.Generation.Interfaces
{
    public interface IFreeSpaceSaver
    {
        public void Add(GeneratorFreeSpaceDto item);
        public List<GeneratorFreeSpaceDto> GetAll();
        public void Clear();
    }
}
