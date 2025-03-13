using IronGrip.Data;
using IronGrip.Helpers;
using IronGrip.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGrip.Repositories
{
    public class AuthRepository
    {

        IronGripContext context;

        public AuthRepository(IronGripContext context)
        {
            this.context = context;
        }


        public async Task<Usuario> LoginAsync(string email, string pwd)
        {
            if (pwd == null)  //Aquí me dejaba encriptar y guardar una contraseña nula, por algún motivo.
            { throw new ArgumentNullException(); }
            var consulta = from datos in this.context.Usuarios
                           where datos.Email == email
                           select datos;

            Usuario user = await consulta.FirstOrDefaultAsync();
            
            if(user != null)
            {
                string salt = user.Salt;
                byte[] tempEncrypt = HelperCryptography.EncryptPassword(pwd, salt);
                byte[] pwdBytes = user.Password;
                bool comparacion = HelperCryptography.CompareArrays(tempEncrypt, pwdBytes);

                if (comparacion)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task RegisterAsync(string email, string nombre, string pwd) 
        {
            Usuario user = new Usuario();
            user.Id = await GetMaxIdAsync();
            user.Nombre = nombre;
            user.Email = email;
            user.Imagen = "default.png";
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(pwd, user.Salt);
            context.Add(user);
            await context.SaveChangesAsync();
        }


        public async Task<int> GetMaxIdAsync()
        {
            if(this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                int max = await this.context.Usuarios.MaxAsync(user => user.Id);
                return max + 1;
            }
        }
    }
}
