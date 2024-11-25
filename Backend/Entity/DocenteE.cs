using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DocenteE
    {
        public int IdDocente { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public DocenteE() { }
        public DocenteE(int _idDocente, string _nombre, string _apellidoPaterno, string _apellidoMaterno, string _nroDocumento, DateTime _fechaNacimiento,
                        string _direccion, string _email, string _celular ) 
        {
            IdDocente = _idDocente;
            Nombres = _nombre;
            ApellidoPaterno = _apellidoPaterno;
            ApellidoMaterno = _apellidoMaterno;
            NroDocumento = _nroDocumento;
            FechaNacimiento = _fechaNacimiento;
            Direccion = _direccion;
            Email = _email;
            Celular = _celular;
        }
    }
}
