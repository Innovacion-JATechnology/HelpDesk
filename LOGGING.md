# 📋 Sistema de Logging - HelpDesk

## Ubicación de los Logs

Los logs se guardan automáticamente en la carpeta `~/Logs` del proyecto:

**Ruta completa:**
```
C:\Users\cepesmar\Desktop\JATechnology\Proyecto HelpDesk\HelpDesk\HelpDesk\Logs
```

## Tipos de Logs

### 1. **error_YYYY-MM-DD.log**
- Contiene todos los errores ocurridos en el día
- Incluye stack trace completo
- Formato: `[TIMESTAMP] ERROR: mensaje`

**Ejemplo:**
```
[2024-03-25 14:32:15.123] ERROR: Error sending email for ticket 1001 to agent@example.com
Exception: SmtpException
Message: Mailbox unavailable. The server response was: 5.1.1 bad address syntax
StackTrace: at System.Net.Mail.SmtpClient.Send(MailMessage message)
```

### 2. **info_YYYY-MM-DD.log**
- Operaciones exitosas
- Asignaciones de tickets
- Actualizaciones de estado

**Ejemplo:**
```
[2024-03-25 14:32:10.456] INFO: Ticket 1001 assigned to Agent 5. Reassigned: false
[2024-03-25 14:32:11.789] INFO: Email queued successfully for ticket 1001 to gabriel@example.com
```

### 3. **warning_YYYY-MM-DD.log**
- Eventos que requieren atención pero no son errores críticos

## Cómo Acceder a los Logs

### Opción 1: Explorador de Archivos (Más Fácil)
1. Abre el Explorador de Windows
2. Navega a: `C:\Users\cepesmar\Desktop\JATechnology\Proyecto HelpDesk\HelpDesk\HelpDesk\Logs`
3. Abre el archivo `.log` que necesites (Bloc de Notas o Editor)

### Opción 2: Visual Studio
1. En Visual Studio, abre `Solution Explorer`
2. Haz clic derecho en el proyecto
3. Selecciona `Open Folder in File Explorer`
4. Navega a la carpeta `Logs`

### Opción 3: Desde la Terminal PowerShell
```powershell
# Navega a la carpeta de logs
cd "C:\Users\cepesmar\Desktop\JATechnology\Proyecto HelpDesk\HelpDesk\HelpDesk\Logs"

# Lista los archivos de log
Get-ChildItem -Filter "*.log" | Sort-Object LastWriteTime -Descending

# Ver el contenido del log más reciente
$latest = Get-ChildItem -Filter "*.log" | Sort-Object LastWriteTime -Descending | Select-Object -First 1
Get-Content $latest.FullName -Tail 50  # Últimas 50 líneas
```

### Opción 4: Monitorear Logs en Tiempo Real
```powershell
# Ver logs en tiempo real (PowerShell)
Get-Content "C:\Users\cepesmar\Desktop\JATechnology\Proyecto HelpDesk\HelpDesk\HelpDesk\Logs\error_2024-03-25.log" -Wait
```

## Eventos Registrados

### ✅ Asignación de Ticket (INFO)
```
Ticket {ticketId} assigned to Agent {agenteId}. Reassigned: {true/false}
Email queued successfully for ticket {ticketId} to {email}
```

### ❌ Error en Asignación (ERROR)
```
Error assigning ticket {ticketId} to agent {agenteId}
Error sending email for ticket {ticketId} to {email}
```

### ✅ Actualización de Estatus (INFO)
```
Agent {agenteId} status updated to Activo/Inactivo
```

### ❌ Error en Actualización (ERROR)
```
Error updating status for agent {agenteId} to {estatus}
```

## Formato de Timestamp

Todos los logs incluyen timestamp en formato:
```
YYYY-MM-DD HH:mm:ss.fff
```

Ejemplo: `2024-03-25 14:32:15.123`

## Estructura de Archivos

Los logs se organizan en **archivos diarios** con el siguiente patrón:

```
Logs/
├── error_2024-03-20.log      (Archivos eliminados automáticamente después de 30 días)
├── error_2024-03-21.log      (Archivos que se conservan)
├── error_2024-03-22.log
├── ...
├── error_2024-03-25.log      (Hoy)
├── info_2024-03-20.log
├── info_2024-03-21.log
├── ...
├── info_2024-03-25.log
├── advertencia_2024-03-20.log
├── advertencia_2024-03-21.log
└── advertencia_2024-03-25.log
```

**Características del sistema de archivos:**
- 📅 **Un archivo por día**: Se crea automáticamente para cada fecha (YYYY-MM-DD)
- 🗑️ **Retención automática**: Los archivos se mantienen durante 30 días
- 🔄 **Rotación diaria**: Un nuevo archivo se crea cada día a las 00:00
- 📊 **Tres tipos de logs**: error, info, y advertencia (uno de cada por día)
- 🔒 **Thread-safe**: Múltiples procesos pueden escribir simultáneamente

## Limpieza de Logs

✅ **La limpieza es automática** - El sistema elimina automáticamente los logs más antiguos de 30 días cuando la aplicación inicia.

**Detalles:**
- Se ejecuta en el constructor estático de la clase `Logger`
- Ocurre una sola vez por sesión de la aplicación
- No interrumpe el funcionamiento de la aplicación
- Los archivos más antiguos de 30 días se eliminan silenciosamente

**Limpieza manual (opcional):**

Si necesitas limpiar manualmente logs más antiguos de 30 días:

```powershell
# Eliminar logs más antiguos de 30 días
$logPath = "C:\Users\cepesmar\Desktop\JATechnology\Proyecto HelpDesk\HelpDesk\HelpDesk\Logs"
Get-ChildItem $logPath -Filter "*.log" | Where-Object {$_.LastWriteTime -lt (Get-Date).AddDays(-30)} | Remove-Item
```

## Notas Importantes

⚠️ **La carpeta Logs se crea automáticamente** la primera vez que ocurra un error
- Si no ves la carpeta, ocurre cuando hay un error o cuando el app inicia
- Los logs no se crean si no hay actividad

✅ **Thread-Safe**: El sistema de logging es seguro para usar desde múltiples threads
- Usa `lock` internamente para evitar conflictos de escritura

📝 **Persistencia**: Los logs persisten entre reinicios de la aplicación
- Perfectos para auditoría y debugging post-mortem
