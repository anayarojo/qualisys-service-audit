## Qualisys Service Audit

Service to validate the right functioning of other Windows services.

#### Basic configuration

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="Services" type="QualisysServiceAudit.Sections.ServiceSection, QualisysServiceAudit" />
  </configSections>
  <Services>
      <Service 
        Index="1" 
        DisplayName="ServiceDisplayName" 
        Name="ServiceName" 
      />
  </Services>
  <appSettings>
    <!-- LOG -->
    <add key="ShowConsole" value="true" />
    <add key="SaveEventLog" value="true" />
    <add key="FullLog" value="true" />
    <add key="LogName" value="ServiceAudit" />
  </appSettings>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
</configuration>
```