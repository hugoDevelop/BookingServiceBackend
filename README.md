# BookingServiceBackend

Este es un proyecto enfocado en un servicio de reservas.

## 2. Backend

### Tareas

- **Booking Reservations**: Endpoint para hacer una nueva reserva, que involucra la creaci�n de registros de reserva y su vinculaci�n con registros de clientes y servicios.
- **Modifying Reservations**: Endpoint para actualizar los detalles de una reserva.
- **Cancelling Reservations**: Endpoint para eliminar una reserva.
- **Viewing Reservations**: Endpoint para recuperar reservas, con capacidades para filtrar por fecha, servicio o cliente.
- **Data Management**: Implementar operaciones que gestionen relaciones complejas y aseguren la integridad en el manejo de datos.

### Manejo de Errores

- Implementaci�n de un manejo robusto de errores, asegurando que los errores se manejen adecuadamente y se proporcionen respuestas claras y significativas a los clientes de la API.
- Uso de excepciones personalizadas y registro (logging) de errores para facilitar la depuraci�n.

## 4. Dockerizaci�n

### Tareas

- Incluir un Dockerfile que permita ejecutar el servicio backend dentro de un contenedor Docker.
- Proporcionar instrucciones claras sobre c�mo construir y ejecutar la aplicaci�n usando Docker.

### Instrucciones

1. **Construir la imagen Docker**:
   docker build -t bookingservicebackend .

2. **Ejecutar el contenedor Docker**:
   docker-compose up

3. **Acceder a la aplicaci�n**:
   La aplicaci�n estar� disponible en `http://localhost:8080`.

## 5. Manejo y Gesti�n de Errores

### Tareas

- Implementar un manejo de errores robusto tanto en el backend como en el frontend.
- Asegurar que los errores se manejen adecuadamente y se proporcionen mensajes claros y significativos a los usuarios y a los clientes de la API.

### Implementaci�n

- **Excepciones Personalizadas**: Se han creado excepciones personalizadas para manejar diferentes tipos de errores en el backend.
- **Respuestas de Error HTTP**: El backend devuelve respuestas HTTP apropiadas para diferentes tipos de errores.
- **Mensajes de Error en el Frontend**: El frontend muestra mensajes de error �tiles y claros a los usuarios.

### Ejemplo de C�digo

// Ejemplo de una excepci�n personalizada public class ReservationNotFoundException : Exception { public ReservationNotFoundException(string message) : base(message) { } }
// Ejemplo de manejo de errores en un controlador [ApiController] [Route("api/[controller]")] public class ReservationsController : ControllerBase { [HttpGet("{id}")] public IActionResult GetReservation(int id) { try { var reservation = _reservationService.GetReservationById(id); if (reservation == null) { throw new ReservationNotFoundException("Reservation not found."); } return Ok(reservation); } catch (ReservationNotFoundException ex) { return NotFound(new { message = ex.Message }); } catch (Exception ex) { _logger.LogError(ex, "An error occurred while getting the reservation."); return StatusCode(500, "Internal server error."); } } }


## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o un pull request para discutir cualquier cambio que te gustar�a hacer.



