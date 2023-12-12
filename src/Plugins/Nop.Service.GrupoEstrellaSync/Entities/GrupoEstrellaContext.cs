using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class GrupoEstrellaContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public GrupoEstrellaContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            Configuration = configuration;
        }

        public GrupoEstrellaContext(DbContextOptions<GrupoEstrellaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Lote> Lotes { get; set; }
        public virtual DbSet<MaestroDeBodega> MaestroDeBodegas { get; set; }
        public virtual DbSet<MaestroProducto> MaestroProductos { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<PedidosDet> PedidosDets { get; set; }
        public virtual DbSet<PedidosEnc> PedidosEncs { get; set; }
        public virtual DbSet<TipoDeProducto> TipoDeProductos { get; set; }
        public virtual DbSet<Ubicacione> Ubicaciones { get; set; }
        public virtual DbSet<PreciosProducto> PreciosProductos { get; set; }
        public virtual DbSet<ClientePersona> ClientePersonas { get; set; }
        public virtual DbSet<ClienteVendedor> ClienteVendedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                string con = Configuration.GetConnectionString("StardentDatabase");
                optionsBuilder.UseSqlServer(con);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientePersona>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CLIENTE_PERSONA");

                entity.Property(e => e.CodigoDeCliente).HasColumnName("CODIGO_DE_CLIENTE");

                entity.Property(e => e.CodigoProfesion)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_PROFESION")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.DescripcionTarjeta)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_TARJETA")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_CIVIL")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_NACIMIENTO");

                entity.Property(e => e.NumeroDeHijos).HasColumnName("NUMERO_DE_HIJOS");

                entity.Property(e => e.NumeroId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NUMERO_ID")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.NumeroTarjeta)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NUMERO_TARJETA")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.RegistroId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("REGISTRO_ID")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.TipoDeVivienda)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_DE_VIVIENDA")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");
            });


            modelBuilder.Entity<ClienteVendedor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CLIENTE_VENDEDOR");

                entity.Property(e => e.CodigoDeCliente).HasColumnName("CODIGO_DE_CLIENTE");

                entity.Property(e => e.CodigoVendedor).HasColumnName("CODIGO_VENDEDOR");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CLIENTES");

                entity.Property(e => e.ActivoEnWeb)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACTIVO_EN_WEB")
                    .IsFixedLength(true);

                entity.Property(e => e.CategoriaCliente)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORIA_CLIENTE");

                entity.Property(e => e.CedulaCliente)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("CEDULA_CLIENTE");

                entity.Property(e => e.ClaveCliente)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CLAVE_CLIENTE");

                entity.Property(e => e.CodigoCobrador).HasColumnName("CODIGO_COBRADOR");

                entity.Property(e => e.CodigoDeClase)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_CLASE")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeCliente).HasColumnName("CODIGO_DE_CLIENTE");

                entity.Property(e => e.CodigoDeCondicion).HasColumnName("CODIGO_DE_CONDICION");

                entity.Property(e => e.CodigoDeGrupo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_GRUPO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeMoneda)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_MONEDA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDePais).HasColumnName("CODIGO_DE_PAIS");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("CODIGO_DEPARTAMENTO");

                entity.Property(e => e.CodigoEcc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_ECC");

                entity.Property(e => e.CodigoImpuesto)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_IMPUESTO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoMunicipio).HasColumnName("CODIGO_MUNICIPIO");

                entity.Property(e => e.CodigoRutaDespacho).HasColumnName("CODIGO_RUTA_DESPACHO");

                entity.Property(e => e.CodigoTerritorio).HasColumnName("CODIGO_TERRITORIO");

                entity.Property(e => e.CodigoTipoPago)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_TIPO_PAGO")
                    .IsFixedLength(true);

                entity.Property(e => e.ConsumoInterno)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CONSUMO_INTERNO")
                    .IsFixedLength(true);

                entity.Property(e => e.ContactoCliente)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTO_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.CtrlIvaPercibido)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CTRL_IVA_PERCIBIDO")
                    .IsFixedLength(true);

                entity.Property(e => e.DireccionCliente)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.DireccionCobro)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_COBRO");

                entity.Property(e => e.DireccionEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_EMAIL");

                entity.Property(e => e.DireccionEnvio)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_ENVIO")
                    .IsFixedLength(true);

                entity.Property(e => e.EstadoCliente)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.ExentImptoCliente)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EXENT_IMPTO_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaApertura)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_APERTURA");

                //entity.Property(e => e.FechaNacimiento)
                //    .HasColumnType("datetime")
                //    .HasColumnName("FECHA_NACIMIENTO");

                entity.Property(e => e.GiroDeNegocio)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("GIRO_DE_NEGOCIO");

                entity.Property(e => e.LimiteCredCliente)
                    .HasColumnType("money")
                    .HasColumnName("LIMITE_CRED_CLIENTE");

                entity.Property(e => e.NitCliente)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NIT_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.NivelPrecio)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NIVEL_PRECIO")
                    .IsFixedLength(true);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMERCIAL");

                entity.Property(e => e.NumeroRegistro)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NUMERO_REGISTRO")
                    .IsFixedLength(true);

                entity.Property(e => e.PorcMora).HasColumnName("PORC_MORA");

                entity.Property(e => e.PorcentajeDescuento).HasColumnName("PORCENTAJE_DESCUENTO");

                entity.Property(e => e.PreciosConIva)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRECIOS_CON_IVA")
                    .IsFixedLength(true);

                entity.Property(e => e.ReferenciaCliente)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("REFERENCIA_CLIENTE");

                entity.Property(e => e.SaldoCliente)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_CLIENTE");

                entity.Property(e => e.SaldoFinanciamiento)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_FINANCIAMIENTO");

                entity.Property(e => e.TelefonoCliente)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.TelexOFaxCliente)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TELEX_O_FAX_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.TipoDeCliente)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_DE_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.TotalChequesRech).HasColumnName("TOTAL_CHEQUES_RECH");

                entity.Property(e => e.TpoDespachoCliente)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TPO_DESPACHO_CLIENTE")
                    .IsFixedLength(true);

                entity.Property(e => e.TpoDespachoParcial)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TPO_DESPACHO_PARCIAL");
            });

            modelBuilder.Entity<Lote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOTES");

                entity.Property(e => e.CantidadDisponible)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_DISPONIBLE");

                entity.Property(e => e.CantidadOrdenado)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_ORDENADO");

                entity.Property(e => e.CantidadReservada)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_RESERVADA");

                entity.Property(e => e.CodigoBodega)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeCompra).HasColumnName("CODIGO_DE_COMPRA");

                entity.Property(e => e.CodigoDeLote)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_LOTE")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeUbicacion)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_UBICACION")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoTipoCompra).HasColumnName("CODIGO_TIPO_COMPRA");

                entity.Property(e => e.CostoPepsUeps).HasColumnName("COSTO_PEPS_UEPS");

                entity.Property(e => e.DescripcionLote)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_LOTE")
                    .IsFixedLength(true);

                entity.Property(e => e.FecVencimientoLote)
                    .HasColumnType("datetime")
                    .HasColumnName("FEC_VENCIMIENTO_LOTE");

                entity.Property(e => e.FechaIngresoLote)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_INGRESO_LOTE");

                entity.Property(e => e.IdCentroComp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ID_CENTRO_COMP")
                    .IsFixedLength(true);

                entity.Property(e => e.IdEmpresaComp)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_EMPRESA_COMP")
                    .IsFixedLength(true);

                entity.Property(e => e.IdSucursalComp)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_SUCURSAL_COMP")
                    .IsFixedLength(true);

                entity.Property(e => e.PesoDisponible)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PESO_DISPONIBLE");

                entity.Property(e => e.Product0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT0")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<MaestroDeBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MAESTRO_DE_BODEGAS");

                entity.Property(e => e.CapacidadVolBodega).HasColumnName("CAPACIDAD_VOL_BODEGA");

                entity.Property(e => e.CodigoBodega)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoCentro)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_CENTRO");

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.DescripcionBodega)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.DireccionBodega)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.Facturacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FACTURACION")
                    .IsFixedLength(true);

                entity.Property(e => e.IdEmpresa)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_EMPRESA")
                    .IsFixedLength(true);

                entity.Property(e => e.IdSucursal)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_SUCURSAL")
                    .IsFixedLength(true);

                entity.Property(e => e.NombreEncargado)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ENCARGADO")
                    .IsFixedLength(true);

                entity.Property(e => e.Planeacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PLANEACION")
                    .IsFixedLength(true);

                entity.Property(e => e.TelefonoBodega)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.TipoBodega)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_BODEGA")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MaestroProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MAESTRO_PRODUCTOS");

                entity.Property(e => e.ActivoEnWeb)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACTIVO_EN_WEB")
                    .IsFixedLength(true);

                entity.Property(e => e.Barrera).HasColumnName("BARRERA");

                entity.Property(e => e.CalculaPrecioCosto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CALCULA_PRECIO_COSTO");

                entity.Property(e => e.CalculoDeReorden)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CALCULO_DE_REORDEN")
                    .IsFixedLength(true);

                entity.Property(e => e.CantMinimaPedido)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANT_MINIMA_PEDIDO");

                entity.Property(e => e.CantMultiploPedido)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANT_MULTIPLO_PEDIDO");

                entity.Property(e => e.ClaseProducto)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CLASE_PRODUCTO");

                entity.Property(e => e.CodigoAbc)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_ABC")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoAnterior)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_ANTERIOR");

                entity.Property(e => e.CodigoArancelario)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_ARANCELARIO");

                entity.Property(e => e.CodigoCompuesto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_COMPUESTO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoEstructuraCi)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_ESTRUCTURA_CI");

                entity.Property(e => e.CodigoMarca)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_MARCA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoReferencia)
                    .IsRequired()
                    .HasMaxLength(26)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_REFERENCIA");

                entity.Property(e => e.CodigoSucesor)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_SUCESOR");

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.CostoAlterno).HasColumnName("COSTO_ALTERNO");

                entity.Property(e => e.CostoUnitario).HasColumnName("COSTO_UNITARIO");

                entity.Property(e => e.CtrlIvaPercibido)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CTRL_IVA_PERCIBIDO")
                    .IsFixedLength(true);

                entity.Property(e => e.DemAntesBarrera)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DEM_ANTES_BARRERA")
                    .IsFixedLength(true);

                entity.Property(e => e.DemDespuesBarrera)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DEM_DESPUES_BARRERA")
                    .IsFixedLength(true);

                entity.Property(e => e.DescripLargaProd)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIP_LARGA_PROD")
                    .IsFixedLength(true);

                entity.Property(e => e.DescripcionProd)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_PROD")
                    .IsFixedLength(true);

                entity.Property(e => e.DiaConsumoPromedio).HasColumnName("DIA_CONSUMO_PROMEDIO");

                entity.Property(e => e.DiaReabastecimiento).HasColumnName("DIA_REABASTECIMIENTO");

                entity.Property(e => e.EstadoProducto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_PRODUCTO")
                    .IsFixedLength(true);

                entity.Property(e => e.ExistenciaMaxima)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("EXISTENCIA_MAXIMA");

                entity.Property(e => e.ExistenciaMinima)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("EXISTENCIA_MINIMA");

                entity.Property(e => e.FactorDeRotacion).HasColumnName("FACTOR_DE_ROTACION");

                entity.Property(e => e.Familia)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("FAMILIA");

                entity.Property(e => e.FechaApertura)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_APERTURA");

                entity.Property(e => e.FechaUltimaEntrada)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ULTIMA_ENTRADA");

                entity.Property(e => e.FechaUltimaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ULTIMA_SALIDA");

                entity.Property(e => e.FechaUltimoConteo)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ULTIMO_CONTEO");

                entity.Property(e => e.FormaDistribucion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FORMA_DISTRIBUCION")
                    .IsFixedLength(true);

                entity.Property(e => e.GrupoConteo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("GRUPO_CONTEO")
                    .IsFixedLength(true);

                entity.Property(e => e.IndicaProcedencia)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("INDICA_PROCEDENCIA")
                    .IsFixedLength(true);

                entity.Property(e => e.ManejaLotes)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MANEJA_LOTES")
                    .IsFixedLength(true);

                entity.Property(e => e.MaximoAVender)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("MAXIMO_A_VENDER");

                entity.Property(e => e.OrigenProductoPlan)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ORIGEN_PRODUCTO_PLAN")
                    .IsFixedLength(true);

                entity.Property(e => e.PagaImpuestos)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PAGA_IMPUESTOS")
                    .IsFixedLength(true);

                entity.Property(e => e.ParticipaPlaneacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PARTICIPA_PLANEACION")
                    .IsFixedLength(true);

                entity.Property(e => e.PorcMaxPeso).HasColumnName("PORC_MAX_PESO");

                entity.Property(e => e.PorcMinPeso).HasColumnName("PORC_MIN_PESO");

                entity.Property(e => e.PorcentajeDescuento).HasColumnName("PORCENTAJE_DESCUENTO");

                entity.Property(e => e.Product0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT0")
                    .IsFixedLength(true);

                entity.Property(e => e.PuntoDeReorden)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PUNTO_DE_REORDEN");

                entity.Property(e => e.Subfamilia)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("SUBFAMILIA");

                entity.Property(e => e.Subsubfamilia)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("SUBSUBFAMILIA");

                entity.Property(e => e.SujetoADescuento)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUJETO_A_DESCUENTO")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.TipoCompetencia)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_COMPETENCIA")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoCostoAValuar)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_COSTO_A_VALUAR")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoDeVenta)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_DE_VENTA")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoExistencia)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_EXISTENCIA")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoExistenciaMin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_EXISTENCIA_MIN")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoProducto)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_PRODUCTO")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MARCAS");

                entity.Property(e => e.CodigoMarca)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_MARCA")
                    .IsFixedLength(true);

                entity.Property(e => e.DescripcionMarca)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_MARCA");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.ValorOmisionMarca)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALOR_OMISION_MARCA")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PedidosDet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PEDIDOS_DET");

                entity.Property(e => e.CantidadDespachada)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_DESPACHADA");

                entity.Property(e => e.CantidadPedida)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_PEDIDA");

                entity.Property(e => e.CodigoUnidadVenta)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_UNIDAD_VENTA")
                    .IsFixedLength(true);

                entity.Property(e => e.CorrelativoIngreso).HasColumnName("CORRELATIVO_INGRESO");

                entity.Property(e => e.FactorConversionUn)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("FACTOR_CONVERSION_UN");

                entity.Property(e => e.IdCentroOperativo)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ID_CENTRO_OPERATIVO")
                    .IsFixedLength(true);

                entity.Property(e => e.IdEmpresa)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_EMPRESA")
                    .IsFixedLength(true);

                entity.Property(e => e.IdSucursal)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_SUCURSAL")
                    .IsFixedLength(true);

                entity.Property(e => e.MontoDescuentoGlob)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_DESCUENTO_GLOB");

                entity.Property(e => e.MontoDescuentoPdet)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_DESCUENTO_PDET");

                entity.Property(e => e.MontoIva)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_IVA");

                entity.Property(e => e.NumeroDePedido).HasColumnName("NUMERO_DE_PEDIDO");

                entity.Property(e => e.PorcDescuentoLinea).HasColumnName("PORC_DESCUENTO_LINEA");

                entity.Property(e => e.PrecioAfectado)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_AFECTADO");

                entity.Property(e => e.PrecioDeLista).HasColumnName("PRECIO_DE_LISTA");

                entity.Property(e => e.PrecioSugerido)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_SUGERIDO");

                entity.Property(e => e.PrecioUnidadVenta)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_UNIDAD_VENTA");

                entity.Property(e => e.Product0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT0")
                    .IsFixedLength(true);

                entity.Property(e => e.SubtotalVentas)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOTAL_VENTAS");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.UsoClave)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USO_CLAVE");
            });

            modelBuilder.Entity<PedidosEnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PEDIDOS_ENC");

                entity.Property(e => e.CambMonedLocalPed).HasColumnName("CAMB_MONED_LOCAL_PED");

                entity.Property(e => e.CodigoDeBodega)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeCliente).HasColumnName("CODIGO_DE_CLIENTE");

                entity.Property(e => e.CodigoDeCondicion).HasColumnName("CODIGO_DE_CONDICION");

                entity.Property(e => e.CodigoRazon)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_RAZON")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoTipoPago)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_TIPO_PAGO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoVendedor).HasColumnName("CODIGO_VENDEDOR");

                entity.Property(e => e.DespachoAutomatico)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DESPACHO_AUTOMATICO")
                    .IsFixedLength(true);

                entity.Property(e => e.DireccionFacturar)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_FACTURAR")
                    .IsFixedLength(true);

                entity.Property(e => e.DocumentoAImprimir)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENTO_A_IMPRIMIR")
                    .IsFixedLength(true);

                entity.Property(e => e.EstadoPedido)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_PEDIDO")
                    .IsFixedLength(true);

                entity.Property(e => e.FactNumLetras).HasColumnName("FACT_NUM_LETRAS");

                entity.Property(e => e.FechaIngresoPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_INGRESO_PEDIDO");

                entity.Property(e => e.FechaOfrecido)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_OFRECIDO");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_PEDIDO");

                entity.Property(e => e.FleteACargoDe)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FLETE_A_CARGO_DE")
                    .IsFixedLength(true);

                entity.Property(e => e.GiroNegocioAFactu)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("GIRO_NEGOCIO_A_FACTU");

                entity.Property(e => e.IdCentroOperativo)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ID_CENTRO_OPERATIVO")
                    .IsFixedLength(true);

                entity.Property(e => e.IdEmpresa)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_EMPRESA")
                    .IsFixedLength(true);

                entity.Property(e => e.IdSucursal)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ID_SUCURSAL")
                    .IsFixedLength(true);

                entity.Property(e => e.IvaPedido)
                    .HasColumnType("money")
                    .HasColumnName("IVA_PEDIDO");

                entity.Property(e => e.MontoCheques)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_CHEQUES");

                entity.Property(e => e.MontoDescuentoLine)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_DESCUENTO_LINE");

                entity.Property(e => e.MontoDescuentoPedi)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_DESCUENTO_PEDI");

                entity.Property(e => e.MontoEfectivo)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_EFECTIVO");

                entity.Property(e => e.MontoEnTarjetas)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_EN_TARJETAS");

                entity.Property(e => e.MontoFlete)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_FLETE");

                entity.Property(e => e.MontoSeguro)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_SEGURO");

                entity.Property(e => e.NitAFacturar)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NIT_A_FACTURAR")
                    .IsFixedLength(true);

                entity.Property(e => e.NivelPrecio)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NIVEL_PRECIO")
                    .IsFixedLength(true);

                entity.Property(e => e.NoDespacho).HasColumnName("NO_DESPACHO");

                entity.Property(e => e.NombreAFacturar)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_A_FACTURAR")
                    .IsFixedLength(true);

                entity.Property(e => e.Nota1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOTA1");

                entity.Property(e => e.Nota2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOTA2");

                entity.Property(e => e.Nota3)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOTA3");

                entity.Property(e => e.Nota4)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOTA4");

                entity.Property(e => e.NumeroCuadre).HasColumnName("NUMERO_CUADRE");

                entity.Property(e => e.NumeroDePedido).HasColumnName("NUMERO_DE_PEDIDO");

                entity.Property(e => e.NumeroPedidoWeb).HasColumnName("NUMERO_PEDIDO_WEB");

                entity.Property(e => e.ObservacionAnula)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACION_ANULA");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.Property(e => e.OrdenCompraRef)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ORDEN_COMPRA_REF");

                entity.Property(e => e.ParticipaPromocion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PARTICIPA_PROMOCION");

                entity.Property(e => e.PorcDescuentoGlob).HasColumnName("PORC_DESCUENTO_GLOB");

                entity.Property(e => e.PorcentajeDeIva).HasColumnName("PORCENTAJE_DE_IVA");

                entity.Property(e => e.RegistroAFacturar)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REGISTRO_A_FACTURAR");

                entity.Property(e => e.SubtotalPedido)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOTAL_PEDIDO");

                entity.Property(e => e.TasaDirectaDolar).HasColumnName("TASA_DIRECTA_DOLAR");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.TipoDeDescuento)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_DE_DESCUENTO")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoEntrega)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_ENTREGA")
                    .IsFixedLength(true);

                entity.Property(e => e.TotalGeneralPedido)
                    .HasColumnType("money")
                    .HasColumnName("TOTAL_GENERAL_PEDIDO");

                entity.Property(e => e.Transporte)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPORTE");

                entity.Property(e => e.UsoClave)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USO_CLAVE");

                entity.Property(e => e.UsuarioIngresoPedi)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESO_PEDI")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TipoDeProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TIPO_DE_PRODUCTO");

                entity.Property(e => e.ClasificacionTipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CLASIFICACION_TIPO")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoTipoProducto).HasColumnName("CODIGO_TIPO_PRODUCTO");

                entity.Property(e => e.ControlSaldos)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CONTROL_SALDOS")
                    .IsFixedLength(true);

                entity.Property(e => e.CtrlCodCompuesto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CTRL_COD_COMPUESTO")
                    .IsFixedLength(true);

                entity.Property(e => e.DescripcionTipo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_TIPO")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<Ubicacione>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UBICACIONES");

                entity.Property(e => e.CapacidadPesoUbica).HasColumnName("CAPACIDAD_PESO_UBICA");

                entity.Property(e => e.CapacidadVolUbica).HasColumnName("CAPACIDAD_VOL_UBICA");

                entity.Property(e => e.CodigoBodega)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_BODEGA")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDeUbicacion)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_UBICACION")
                    .IsFixedLength(true);

                entity.Property(e => e.DescripUbicacion)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIP_UBICACION")
                    .IsFixedLength(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<PreciosProducto>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PRECIOS_PRODUCTOS");

                entity.Property(e => e.CantidadFinal)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_FINAL");

                entity.Property(e => e.CantidadInicial)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("CANTIDAD_INICIAL");

                entity.Property(e => e.CodigoDeMoneda)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DE_MONEDA")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.CodigoUnidadVenta)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_UNIDAD_VENTA")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.FechaVigenciaF)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_VIGENCIA_F");

                entity.Property(e => e.FechaVigenciaI)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_VIGENCIA_I");

                entity.Property(e => e.MargenPrecioMax).HasColumnName("MARGEN_PRECIO_MAX");

                entity.Property(e => e.MargenPrecioMin).HasColumnName("MARGEN_PRECIO_MIN");

                entity.Property(e => e.NivelPrecio)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("NIVEL_PRECIO")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.PrecioEnFuncion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRECIO_EN_FUNCION")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.PrecioMaximo)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_MAXIMO");

                entity.Property(e => e.PrecioMinimo)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_MINIMO");

                entity.Property(e => e.PrecioSugerido)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("PRECIO_SUGERIDO");

                entity.Property(e => e.Product0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT0")
                    .IsFixedLength(true)
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");

                entity.Property(e => e.SecuenciaPrecio).HasColumnName("SECUENCIA_PRECIO");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("timestamp");

                entity.Property(e => e.ValorOMargen)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALOR_O_MARGEN")
                    .HasComment("SQL_Latin1_General_CP1_CS_AS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
