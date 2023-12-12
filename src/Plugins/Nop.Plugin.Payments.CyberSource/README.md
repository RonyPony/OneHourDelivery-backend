# Documentación: plugin CyberSource

En este archivo estaremos visualizando las informaciones correspondientes sobre el plugin de pasarela de pago de CyberSource, este plugin está desarrollado para NopCommerce v4.30
sus funciones son, después de seleccionar los productos como una compra normal aparecerá como un método de pago (después de haberlo instalado), te redireccionará a la página de
pago de CyberSource y se procederá la transacción.

# Compilación e instalación

Para configurar el plugin se necesita montarlo en un NopCommerce correspondiente a la versión del plugin, la 4.30 en este caso, y por consiguiente necesitaremos IIS y SQL Server 
para correr el proyecto desde Visual Studio, una vez hecho esto podremos instalar el plugin desde la administración entrando a Configuración/Local Plugins y deberá aparecer el Plugin
de CyberSource, una vez instalado se hace una compra normal y aparecerá como un método de pago.

# Campos y valores

Existen varios campos necesarios para poder hacer correctamente la transacción con este método de pago, la mayoría configurables desde el admin, o por lo menos los que el cliente
necesita.

>*Están los siguientes campos:*

1. CyberSourcePaymentUrl: En este campo se encuentra la Url que redirecciona al NopCommerce a la página de pago de CyberSource.

2. Acces_key: Llave de acceso a la página de CyberSource proporcionada por el Api del cliente para poder llevar a cabo la transacción.

3. Signed_field_names: Lista de parámetros separados por comas que fueron firmados por el servidor, provenientes del Api igualmente.

4. ProfileId: Id del comercio para el cueal se va a realizar la compra del poducto.

5. Secret_key: Ees la clave que se usa para encriptar los campos que se le envían a la página de pago.

6. SerialNumber: Este campo valida el permiso del comerciante para utilizar dicho método de pago.

7. Transaction_type: Especifica el tipo de transacción que se está realizando.

8. Currency: Tipo de moneda, en el formato "$100.00"

9. Locale: Indicador de lenguaje utilizado.

Recalcando, todos estos valores provienen del Api correspondiente.

# Más información

Para más información visita la siguiente documentación de CyberSource: https://developer.cybersource.com/hello-world/testing-guide.html
