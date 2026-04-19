# ESPAÑOL
-----
**RestoreWorldEz** es un plugin avanzado para servidores **TShock** diseñado para la preservación y restauración de estructuras. Utiliza un formato propio (`.ezsch`) para guardar las estructuras en schematics.

Este plugin es ideal para servidores que requieren que ciertas áreas se mantengan intactas o se restauren automáticamente tras un periodo de tiempo.



## ✨ Características Principales

* **📍 Selección Intuitiva:** Define áreas golpeando bloques en el juego (estilo WorldEdit/Regiones de TShock).
* **📦 Restauración de Contenido:** Guarda y restaura no solo bloques, sino también el contenido de los **cofres** (items, stacks y prefijos), cables y el texto de los **carteles**.
* **🌐 Internacionalización (i18n):** Soporte nativo para **Español (es)**, **Inglés (en)** y **Portugués (pt).
* **⏰ Auto-Restauración:** Sistema de temporizador configurable para restaurar zonas automáticamente (ej: cada 1h, 30m, etc.).
* **📂 Formato .ezsch:** Archivos de esquema ligeros y optimizados que guardan bloques, paredes, líquidos, colores, actuadores y cableado.
* **🔀 Gestión de Variantes:** Capacidad para alternar aleatoriamente entre múltiples esquemas en una misma coordenada o superponerlos.



## 🎮 Comandos y Uso

### Comandos de Administración
* `/rw set 1`: Activa el modo de selección para el **Punto 1** (esquina superior izquierda). Golpea un bloque para fijarlo.
* `/rw set 2`: Activa el modo de selección para el **Punto 2** (esquina inferior derecha). Golpea un bloque para fijarlo.
* `/rw define <nombre>`: Guarda el área seleccionada en la carpeta de esquemas.
* `/rw restore`: Ejecuta manualmente la restauración de todas las zonas configuradas.
* `/rw update`: Recarga el archivo de configuración `RestoreConfig.json` sin reiniciar el servidor.
* `/restorelang <es|en|pt>`: Cambia el idioma de los mensajes del plugin para tu cuenta.

### Permisos
* `worldrestore.admin`: Permiso necesario para utilizar todos los comandos del plugin.



## ⚙️ Configuración (RestoreConfig.json)

El archivo se genera automáticamente en la carpeta de TShock. Aquí puedes ajustar el comportamiento global:

```json
{
  "DefaultLang": "en",           // Idioma por defecto de la consola y ayuda (es, en, pt)
  "AutoRestore": false,          // Activa la restauración automática por tiempo
  "RestoreInterval": "1h",       // Intervalo (m = minutos, h = horas, d = días)
  "RestoreChests": true,         // ¿Restaurar inventario de cofres?
  "ReplaceServerAir": true,      // ¿Sobrescribir bloques de aire del servidor?
  "BackgroundOverwriteTiles": true, // ¿Sobrescribir bloques del servidor por paredes de fondo?
  "ReplaceWithReferenceAir": false, // ¿Usar el aire del schematic para reemplazar bloques?
  "AnnounceBefore": true,        // Avisar antes de restaurar
  "AnnounceMinutes": 5           // Minutos previos para el aviso
}
```


# ENGLISH
-----
**RestoreWorldEz** is an advanced plugin for **TShock** servers designed for the preservation and restoration of structures. It uses its own custom format (`.ezsch`) to save structures as schematics.

This plugin is ideal for servers that require certain areas to remain intact or be automatically restored after a period of time.

## ✨ Main Features

* **📍 Intuitive Selection:** Define areas by hitting blocks in-game (WorldEdit/TShock Regions style).
* **📦 Content Restoration:** Saves and restores not only blocks, but also **chest** contents (items, stacks, and prefixes), wiring, and **sign** text.
* **🌐 Internationalization (i18n):** Native support for **Spanish (es)**, **English (en)**, and **Portuguese (pt)**.
* **⏰ Auto-Restoration:** Configurable timer system to automatically restore zones (e.g., every 1h, 30m, etc.).
* **📂 .ezsch Format:** Lightweight and optimized schematic files that save blocks, walls, liquids, paint colors, actuators, and wiring.
* **🔀 Variant Management:** Ability to randomly alternate between multiple schematics at the same coordinates or overlay them.

## 🎮 Commands and Usage

### Admin Commands
* `/rw set 1`: Activates selection mode for **Point 1** (top-left corner). Hit a block to set it.
* `/rw set 2`: Activates selection mode for **Point 2** (bottom-right corner). Hit a block to set it.
* `/rw define <name>`: Saves the selected area in the schematics folder.
* `/rw restore`: Manually executes the restoration of all configured zones.
* `/rw update`: Reloads the `RestoreConfig.json` configuration file without restarting the server.
* `/restorelang <es|en|pt>`: Changes the plugin's message language for your account.

### Permissions
* `worldrestore.admin`: Permission required to use all plugin commands.

## ⚙️ Configuration (RestoreConfig.json)

The file is automatically generated in the TShock folder. Here you can adjust the global behavior:

```json
{
  "DefaultLang": "en",           // Default console and help language (es, en, pt)
  "AutoRestore": false,          // Enables automatic time-based restoration
  "RestoreInterval": "1h",       // Interval (m = minutes, h = hours, d = days)
  "RestoreChests": true,         // Restore chest inventories?
  "ReplaceServerAir": true,      // Overwrite server air blocks?
  "BackgroundOverwriteTiles": true, // Overwrite server blocks with background walls?
  "ReplaceWithReferenceAir": false, // Use schematic air to replace server blocks?
  "AnnounceBefore": true,        // Announce before restoring
  "AnnounceMinutes": 5           // Minutes prior for the announcement
}
