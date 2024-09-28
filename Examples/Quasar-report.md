
---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmConnections (../Quasar/Quasar.Server/Forms/FrmConnections.cs, line 12) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* TcpConnectionsHandler (../Quasar/Quasar.Server/Messages/TcpConnectionsHandler.cs, line 11) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* RemoteShellHandler (../Quasar/Quasar.Client/Messages/RemoteShellHandler.cs, line 12) -> Facade  
* Shell (../Quasar/Quasar.Client/IO/Shell.cs, line 15) -> Subsystem  
* QuasarClient (../Quasar/Quasar.Client/Networking/QuasarClient.cs, line 17) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmTaskManager (../Quasar/Quasar.Server/Forms/FrmTaskManager.cs, line 14) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* TaskManagerHandler (../Quasar/Quasar.Server/Messages/TaskManagerHandler.cs, line 12) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FileTransfer (../Quasar/Quasar.Server/Models/FileTransfer.cs, line 8) -> Facade  
* SafeRandom (../Quasar/Quasar.Common/Utilities/SafeRandom.cs, line 10) -> Subsystem  
* FileSplit (../Quasar/Quasar.Common/IO/FileSplit.cs, line 9) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmFileManager (../Quasar/Quasar.Server/Forms/FrmFileManager.Designer.cs, line 5) -> Facade  
* DotNetBarTabControl (../Quasar/Quasar.Server/Controls/DotNetBarTabControl.cs, line 10) -> Subsystem  
* AeroListView (../Quasar/Quasar.Server/Controls/ListViewEx.cs, line 9) -> Subsystem  

---
>                        Decorator Design Pattern
>
>Intent: Lets you attach new behaviors to objects by placing these objects
>inside special wrapper objects that contain the behaviors.    

* IAccountReader (../Quasar/Quasar.Client/Recovery/IPassReader.cs, line 9) -> Base component  
* FileZillaPassReader (../Quasar/Quasar.Client/Recovery/FtpClients/FileZillaPassReader.cs, line 10) -> Concrete component  
* WinScpPassReader (../Quasar/Quasar.Client/Recovery/FtpClients/WinScpPassReader.cs, line 11) -> Concrete component  
* InternetExplorerPassReader (../Quasar/Quasar.Client/Recovery/Browsers/InternetExplorerPassReader.cs, line 16) -> Concrete component  
* FirefoxPassReader (../Quasar/Quasar.Client/Recovery/Browsers/FirefoxPassReader.cs, line 11) -> Concrete component  
* ChromiumBase (../Quasar/Quasar.Client/Recovery/Browsers/ChromiumBase.cs, line 12) -> Base decorator  
* OperaGXPassReader (../Quasar/Quasar.Client/Recovery/Browsers/OperaGXPassReader.cs, line 8) -> Concrete decorator  
* ChromePassReader (../Quasar/Quasar.Client/Recovery/Browsers/ChromePassReader.cs, line 8) -> Concrete decorator  
* OperaPassReader (../Quasar/Quasar.Client/Recovery/Browsers/OperaPassReader.cs, line 8) -> Concrete decorator  
* EdgePassReader (../Quasar/Quasar.Client/Recovery/Browsers/EdgePassReader.cs, line 8) -> Concrete decorator  
* BravePassReader (../Quasar/Quasar.Client/Recovery/Browsers/BravePassReader.cs, line 8) -> Concrete decorator  
* YandexPassReader (../Quasar/Quasar.Client/Recovery/Browsers/YandexPassReader.cs, line 8) -> Concrete decorator  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* EditView (../Quasar/Quasar.Server/Controls/HexEditor/EditView.cs, line 7) -> Facade  
* HexViewHandler (../Quasar/Quasar.Server/Controls/HexEditor/HexViewHandler.cs, line 7) -> Subsystem  
* StringViewHandler (../Quasar/Quasar.Server/Controls/HexEditor/StringViewHandler.cs, line 7) -> Subsystem  
* HexEditor (../Quasar/Quasar.Server/Controls/HexEditor/HexEditor.cs, line 51) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* UnsafeStreamCodec (../Quasar/Quasar.Common/Video/Codecs/UnsafeStreamCodec.cs, line 10) -> Facade  
* Resolution (../Quasar/Quasar.Common/Video/Resolution.cs, line 7) -> Subsystem  
* JpgCompression (../Quasar/Quasar.Common/Video/Compression/JpgCompression.cs, line 8) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmStartupManager (../Quasar/Quasar.Server/Forms/FrmStartupManager.cs, line 14) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* StartupManagerHandler (../Quasar/Quasar.Server/Messages/StartupManagerHandler.cs, line 12) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* ReverseProxyClient (../Quasar/Quasar.Server/ReverseProxy/ReverseProxyClient.cs, line 11) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* ReverseProxyServer (../Quasar/Quasar.Server/ReverseProxy/ReverseProxyServer.cs, line 10) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* HexEditor (../Quasar/Quasar.Server/Controls/HexEditor/HexEditor.cs, line 51) -> Facade  
* IKeyMouseEventHandler (../Quasar/Quasar.Server/Controls/HexEditor/IKeyMouseEventHandler.cs, line 6) -> Subsystem  
* EditView (../Quasar/Quasar.Server/Controls/HexEditor/EditView.cs, line 7) -> Subsystem  
* ByteCollection (../Quasar/Quasar.Server/Controls/HexEditor/ByteCollection.cs, line 6) -> Subsystem  
* Caret (../Quasar/Quasar.Server/Controls/HexEditor/Caret.cs, line 7) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmRemoteDesktop (../Quasar/Quasar.Server/Forms/FrmRemoteDesktop.cs, line 16) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* RemoteDesktopHandler (../Quasar/Quasar.Server/Messages/RemoteDesktopHandler.cs, line 15) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmRegistryEditor (../Quasar/Quasar.Server/Forms/FrmRegistryEditor.Designer.cs, line 5) -> Facade  
* RegistryTreeView (../Quasar/Quasar.Server/Controls/RegistryTreeView.cs, line 5) -> Subsystem  
* AeroListView (../Quasar/Quasar.Server/Controls/ListViewEx.cs, line 9) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmRegistryEditor (../Quasar/Quasar.Server/Forms/FrmRegistryEditor.cs, line 19) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* RegistryHandler (../Quasar/Quasar.Server/Messages/RegistryHandler.cs, line 12) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* KeyloggerHandler (../Quasar/Quasar.Server/Messages/KeyloggerHandler.cs, line 15) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* FileManagerHandler (../Quasar/Quasar.Server/Messages/FileManagerHandler.cs, line 20) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmSystemInformation (../Quasar/Quasar.Server/Forms/FrmSystemInformation.cs, line 13) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* SystemInformationHandler (../Quasar/Quasar.Server/Messages/SystemInformationHandler.cs, line 12) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* RemoteDesktopHandler (../Quasar/Quasar.Server/Messages/RemoteDesktopHandler.cs, line 15) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* UnsafeStreamCodec (../Quasar/Quasar.Common/Video/Codecs/UnsafeStreamCodec.cs, line 10) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* QuasarClient (../Quasar/Quasar.Client/Networking/QuasarClient.cs, line 17) -> Facade  
* HostsManager (../Quasar/Quasar.Common/DNS/HostsManager.cs, line 7) -> Subsystem  
* SafeRandom (../Quasar/Quasar.Common/Utilities/SafeRandom.cs, line 10) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FileManagerHandler (../Quasar/Quasar.Server/Messages/FileManagerHandler.cs, line 20) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* TaskManagerHandler (../Quasar/Quasar.Server/Messages/TaskManagerHandler.cs, line 12) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmKeylogger (../Quasar/Quasar.Server/Forms/FrmKeylogger.cs, line 12) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* KeyloggerHandler (../Quasar/Quasar.Server/Messages/KeyloggerHandler.cs, line 15) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmMain (../Quasar/Quasar.Server/Forms/FrmMain.cs, line 19) -> Facade  
* QuasarServer (../Quasar/Quasar.Server/Networking/QuasarServer.cs, line 11) -> Subsystem  
* ClientStatusHandler (../Quasar/Quasar.Server/Messages/ClientStatusHandler.cs, line 11) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* QuasarApplication (../Quasar/Quasar.Client/QuasarApplication.cs, line 24) -> Facade  
* SingleInstanceMutex (../Quasar/Quasar.Client/Utilities/SingleInstanceMutex.cs, line 9) -> Subsystem  
* QuasarClient (../Quasar/Quasar.Client/Networking/QuasarClient.cs, line 17) -> Subsystem  
* KeyloggerService (../Quasar/Quasar.Client/Logging/KeyloggerService.cs, line 10) -> Subsystem  
* ActivityDetection (../Quasar/Quasar.Client/User/ActivityDetection.cs, line 13) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Facade  
* BufferPool (../Quasar/Quasar.Server/Networking/BufferPool.cs, line 10) -> Subsystem  
* UserState (../Quasar/Quasar.Server/Networking/UserState.cs, line 8) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* ClientServicesHandler (../Quasar/Quasar.Client/Messages/ClientServicesHandler.cs, line 15) -> Facade  
* QuasarClient (../Quasar/Quasar.Client/Networking/QuasarClient.cs, line 17) -> Subsystem  
* QuasarApplication (../Quasar/Quasar.Client/QuasarApplication.cs, line 24) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmBuilder (../Quasar/Quasar.Server/Forms/FrmBuilder.Designer.cs, line 5) -> Facade  
* DotNetBarTabControl (../Quasar/Quasar.Server/Controls/DotNetBarTabControl.cs, line 10) -> Subsystem  
* Line (../Quasar/Quasar.Server/Controls/Line.cs, line 6) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* FrmRemoteShell (../Quasar/Quasar.Server/Forms/FrmRemoteShell.cs, line 12) -> Facade  
* Client (../Quasar/Quasar.Server/Networking/Client.cs, line 11) -> Subsystem  
* RemoteShellHandler (../Quasar/Quasar.Server/Messages/RemoteShellHandler.cs, line 10) -> Subsystem  
