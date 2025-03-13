using IronGrip.Data;
using IronGrip.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGrip.Repositories
{
    public class TagRepository
    {
        IronGripContext context;

        public TagRepository(IronGripContext context) 
        {
            this.context = context;
        }

        public async Task CreateTagAsync(int idUsuario, string nombre, string color) 
        {
            Tag tag = new Tag();
            tag.Id = await GetMaxIdAsync();
            tag.Nombre = nombre;
            tag.Color = color;
            tag.IdUsuario = idUsuario;
            this.context.Tags.Add(tag);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetTagsAsync(int idUsuario)
        {
            List<Tag>  tags = await context.Tags
                .ToListAsync();
            return tags;
        }

        public async Task<int> GetMaxIdAsync()
        {

            if (await this.context.Tags.CountAsync() < 1)
            {
                return 1;
            }
            else
            {
                return await this.context.Tags.MaxAsync(x => x.Id) + 1;
            }
        }      
    }
}
