using System.Collections.Generic;

namespace RestoreWorldEz
{
    public static class Restorei18n
    {
        public static Dictionary<int, string> PlayerLangs = new Dictionary<int, string>();

        private static Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>
        {
            ["es"] = new Dictionary<string, string> {
                {"Set1", "[RW] Punto 1 fijado en X:{0} Y:{1}"},
                {"Set2", "[RW] Punto 2 fijado en X:{0} Y:{1}"},
                {"Hit1", "[RW] Por favor, golpea un bloque para el Punto 1."},
                {"Hit2", "[RW] Por favor, golpea un bloque para el Punto 2."},
                {"DefineSuccess", "¡Estructura '{0}' guardada con éxito!"},
                {"DefineSize", "Dimensiones: {0}x{1} en la posición X:{2} Y:{3}."},
                {"NoPoints", "Faltan puntos. Usa /rw set 1 y /rw set 2 antes de definir."},
                {"DefineUsage", "Uso correcto: /rw define <nombre_sin_espacios>"},
                {"SetUsage", "Uso correcto: /rw set 1 o /rw set 2"},
                {"SetOnly1or2", "Solo puedes usar '1' o '2'."},
                {"LangChanged", "Idioma cambiado a Español."},
                {"Restoring", "Iniciando restauración manual de todas las zonas..."},
                {"RestoreDone", "Restauración completada."},
                {"UpdateConfig", "¡Configuración de RestoreWorldEz recargada desde el JSON!"},
                {"UnknownCmd", "Subcomando no reconocido. Usa /rw para ver la ayuda."},
                {"Help1", "Comandos disponibles:"},
                {"Help2", "/rw set 1 - Marca el primer punto de la estructura."},
                {"Help3", "/rw set 2 - Marca el segundo punto."},
                {"Help4", "/rw define <nombre> - Guarda el área seleccionada."},
                {"Help5", "/rw restore - Ejecuta la restauración masiva de todos los archivos."},
                {"SaveError", "Error al guardar la estructura. Revisa la consola."},
                {"LangUsage", "Uso: /restorelang <en|es|pt>"},
                {"AnnounceRestore", "¡Aviso! La restauración automática del mundo comenzará en {0} minutos."},
                
                // Mensajes de Consola
                {"FolderCreated", "Carpeta 'RestoreSchematics' creada automáticamente en la inicialización."},
                {"NoSchematics", "No se encontraron esquemas .ezsch en la carpeta 'RestoreSchematics'."},
                {"ZonesDetected", "Se detectaron {0} zonas distintas para restaurar."},
                {"PastingSchematic", "Pegando esquema: {0}"},
                {"RestoreSuccess", "¡Proceso de restauración por zonas finalizado con éxito!"},
                {"CriticalError", "Error crítico en RestoreWorld: {0}"},
                {"InvalidFileName", "El archivo {0} no tiene coordenadas en el nombre. Ignorando."},
                {"LoadError", "Error cargando estructura: {0}"},
                {"CmdHelpRW", "Gestión de restauración: /rw set 1|2, /rw define <nombre>, /rw restore"},
                {"CmdHelpLang", "Cambia el idioma de RestoreWorldEz para tu cuenta. Opciones: en, es, pt"},
                {"ConfigReadError", "Error leyendo configuración: {0}"},
                {"InvalidInterval", "Formato de intervalo inválido en el JSON. Usando 1 hora por defecto."},
                {"PluginLoaded", "Plugin cargado correctamente. Usa /rw para gestionar tus estructuras."}
            },
            ["en"] = new Dictionary<string, string> {
                {"Set1", "[RW] Point 1 set at X:{0} Y:{1}"},
                {"Set2", "[RW] Point 2 set at X:{0} Y:{1}"},
                {"Hit1", "[RW] Please hit a block for Point 1."},
                {"Hit2", "[RW] Please hit a block for Point 2."},
                {"DefineSuccess", "Structure '{0}' saved successfully!"},
                {"DefineSize", "Dimensions: {0}x{1} at position X:{2} Y:{3}."},
                {"NoPoints", "Missing points. Use /rw set 1 and /rw set 2 before defining."},
                {"DefineUsage", "Usage: /rw define <name_without_spaces>"},
                {"SetUsage", "Usage: /rw set 1 or /rw set 2"},
                {"SetOnly1or2", "You can only use '1' or '2'."},
                {"LangChanged", "Language changed to English."},
                {"Restoring", "Starting manual restoration of all zones..."},
                {"RestoreDone", "Restoration completed."},
                {"UpdateConfig", "RestoreWorldEz configuration reloaded from JSON!"},
                {"UnknownCmd", "Unknown subcommand. Use /rw for help."},
                {"Help1", "Available commands:"},
                {"Help2", "/rw set 1 - Sets the first point of the structure."},
                {"Help3", "/rw set 2 - Sets the second point."},
                {"Help4", "/rw define <name> - Saves the selected area."},
                {"Help5", "/rw restore - Executes mass restoration of all files."},
                {"SaveError", "Error saving structure. Check the console."},
                {"LangUsage", "Usage: /restorelang <en|es|pt>"},
                {"AnnounceRestore", "Warning! Automatic world restoration will begin in {0} minutes."},
                
                // Console messages
                {"FolderCreated", "'RestoreSchematics' folder created automatically on initialization."},
                {"NoSchematics", "No .ezsch schematics found in the 'RestoreSchematics' folder."},
                {"ZonesDetected", "Detected {0} distinct zones to restore."},
                {"PastingSchematic", "Pasting schematic: {0}"},
                {"RestoreSuccess", "Zone restoration process completed successfully!"},
                {"CriticalError", "Critical error in RestoreWorld: {0}"},
                {"InvalidFileName", "File {0} does not have coordinates in its name. Ignoring."},
                {"LoadError", "Error loading structure: {0}"},
                {"CmdHelpRW", "Restoration management: /rw set 1|2, /rw define <name>, /rw restore"},
                {"CmdHelpLang", "Change the RestoreWorldEz language for your account. Options: en, es, pt"},
                {"ConfigReadError", "Error reading configuration: {0}"},
                {"InvalidInterval", "Invalid interval format in JSON. Using 1 hour by default."},
                {"PluginLoaded", "Plugin loaded successfully. Use /rw to manage your structures."}

            },
            ["pt"] = new Dictionary<string, string> {
                {"Set1", "[RW] Ponto 1 definido em X:{0} Y:{1}"},
                {"Set2", "[RW] Ponto 2 definido em X:{0} Y:{1}"},
                {"Hit1", "[RW] Por favor, bata num bloco para o Ponto 1."},
                {"Hit2", "[RW] Por favor, bata num bloco para o Ponto 2."},
                {"DefineSuccess", "Estrutura '{0}' salva com sucesso!"},
                {"DefineSize", "Dimensões: {0}x{1} na posição X:{2} Y:{3}."},
                {"NoPoints", "Faltam pontos. Use /rw set 1 e /rw set 2 antes de definir."},
                {"DefineUsage", "Uso correto: /rw define <nome_sem_espacos>"},
                {"SetUsage", "Uso correto: /rw set 1 ou /rw set 2"},
                {"SetOnly1or2", "Você só pode usar '1' ou '2'."},
                {"LangChanged", "Idioma alterado para Português."},
                {"Restoring", "Iniciando restauração manual de todas as zonas..."},
                {"RestoreDone", "Restauração concluída."},
                {"UpdateConfig", "Configuração do RestoreWorldEz recarregada do JSON!"},
                {"UnknownCmd", "Subcomando não reconhecido. Use /rw para ajuda."},
                {"Help1", "Comandos disponíveis:"},
                {"Help2", "/rw set 1 - Marca o primeiro ponto da estrutura."},
                {"Help3", "/rw set 2 - Marca o segundo ponto."},
                {"Help4", "/rw define <nome> - Salva a área selecionada."},
                {"Help5", "/rw restore - Executa a restauração em massa de todos os arquivos."},
                {"SaveError", "Erro ao salvar estrutura. Verifique o console."},
                {"LangUsage", "Uso: /restorelang <en|es|pt>"},
                {"AnnounceRestore", "Aviso! A restauração automática do mundo começará em {0} minutos."},

                
                // Mensagens de consola
                {"FolderCreated", "Pasta 'RestoreSchematics' criada automaticamente na inicialização."},
                {"NoSchematics", "Nenhum esquema .ezsch encontrado na pasta 'RestoreSchematics'."},
                {"ZonesDetected", "Detectadas {0} zonas distintas para restaurar."},
                {"PastingSchematic", "Colando esquema: {0}"},
                {"RestoreSuccess", "Processo de restauração de zonas concluído com sucesso!"},
                {"CriticalError", "Erro crítico no RestoreWorld: {0}"},
                {"InvalidFileName", "O arquivo {0} não possui coordenadas no nome. Ignorando."},
                {"LoadError", "Erro ao carregar estrutura: {0}"},
                {"CmdHelpRW", "Gestão de restauração: /rw set 1|2, /rw define <nome>, /rw restore"},
                {"CmdHelpLang", "Muda o idioma do RestoreWorldEz para sua conta. Opções: en, es, pt"},
                {"ConfigReadError", "Erro ao ler a configuração: {0}"},
                {"InvalidInterval", "Formato de intervalo inválido no JSON. Usando 1 hora por padrão."},
                {"PluginLoaded", "Plugin carregado com sucesso. Use /rw para gerenciar suas estruturas."}
            }
        };

        public static string Get(int playerIndex, string key, params object[] args)
        {
            string lang = RestoreCore.Config?.DefaultLang?.ToLower() ?? "en";

            if (PlayerLangs.ContainsKey(playerIndex))
                lang = PlayerLangs[playerIndex];

            if (!Translations.ContainsKey(lang))
                lang = "en";

            if (Translations[lang].ContainsKey(key))
                return string.Format(Translations[lang][key], args);

            return key;
        }
    }
}