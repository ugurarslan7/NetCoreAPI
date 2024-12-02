using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Categories.Dto
{
    public record CategoryDto(int Id, string Name, List<CategoryWithProductsDto> CategoryWithProductsDtos);

}
