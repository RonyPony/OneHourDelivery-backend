using System;
using System.Data.SqlTypes;

namespace Nop.Service.GrupoEstrellaSync.Models
{
    public class UploadClient
    {
        public int? CODIGO_DE_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string ESTADO_CLIENTE { get; set; }
        public string CONTACTO_CLIENTE { get; set; }
        public string DIRECCION_CLIENTE { get; set; }
        public string DIRECCION_ENVIO { get; set; }
        public string TELEFONO_CLIENTE { get; set; }
        public string TELEX_O_FAX_CLIENTE { get; set; }
        public DateTime FECHA_APERTURA { get; set; }
        public string NIT_CLIENTE { get; set; }
        public SqlMoney LIMITE_CRED_CLIENTE { get; set; }
        public SqlMoney SALDO_CLIENTE { get; set; }
        public string EXENT_IMPTO_CLIENTE { get; set; }
        public string TIPO_DE_CLIENTE { get; set; }
        public string TPO_DESPACHO_CLIENTE { get; set; }
        public int CODIGO_DEPARTAMENTO { get; set; }
        public int CODIGO_DE_PAIS { get; set; }
        public int CODIGO_MUNICIPIO { get; set; }
        public int CODIGO_TERRITORIO { get; set; }
        public string CODIGO_DE_CLASE { get; set; }
        public string NIVEL_PRECIO { get; set; }
        public int CODIGO_DE_CONDICION { get; set; }
        public string CODIGO_TIPO_PAGO { get; set; }
        public int TOTAL_CHEQUES_RECH { get; set; }
        public string GIRO_DE_NEGOCIO { get; set; }
        public string NUMERO_REGISTRO { get; set; }
        public string CODIGO_IMPUESTO { get; set; }
        public string CODIGO_DE_MONEDA { get; set; }
        public int CODIGO_COBRADOR { get; set; }
        public string CODIGO_DE_GRUPO { get; set; }
        public float PORCENTAJE_DESCUENTO { get; set; }
        public SqlMoney SALDO_FINANCIAMIENTO { get; set; }
        public string CODIGO_ECC { get; set; }
        public int CODIGO_RUTA_DESPACHO { get; set; }
        // I-Persona Individual como categoria de cliente TODO
        public string CATEGORIA_CLIENTE { get; set; }
        public float PORC_MORA { get; set; }
        public string NOMBRE_COMERCIAL { get; set; }
        public string PRECIOS_CON_IVA { get; set; }
        public string REFERENCIA_CLIENTE { get; set; }
        public string CTRL_IVA_PERCIBIDO { get; set; }
        public string TPO_DESPACHO_PARCIAL { get; set; }
        public string CONSUMO_INTERNO { get; set; }
        public string CEDULA_CLIENTE { get; set; }
        public string DIRECCION_COBRO { get; set; }
        public string DIRECCION_EMAIL { get; set; }
        public string CLAVE_CLIENTE { get; set; }
        public string ACTIVO_EN_WEB { get; set; }
        public DateTime? FECHA_NACIMIENTO { get; set; }

    }
}
