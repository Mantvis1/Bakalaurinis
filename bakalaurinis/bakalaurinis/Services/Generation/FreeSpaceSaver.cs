using bakalaurinis.Dtos;
using bakalaurinis.Services.Generation.Interfaces;
using System.Collections.Generic;

namespace bakalaurinis.Services.Generation
{
    public class FreeSpaceSaver : IFreeSpaceSaver
    {
        private readonly List<GeneratorFreeSpaceDto> _freeSpaces;

        public FreeSpaceSaver()
        {
            _freeSpaces = new List<GeneratorFreeSpaceDto>();
        }

        public void Add(GeneratorFreeSpaceDto item)
        {
            _freeSpaces.Add(item);
        }

        public void Clear()
        {
            _freeSpaces.Clear();
        }

        public List<GeneratorFreeSpaceDto> GetAll()
        {
            return _freeSpaces;
        }
    }
}
