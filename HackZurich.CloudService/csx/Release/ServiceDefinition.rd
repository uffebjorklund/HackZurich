<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HackZurich.CloudService" generation="1" functional="0" release="0" Id="e0ef8479-6463-4959-b580-48e052f16bf8" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HackZurich.CloudServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HackService:Endpoint1" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/LB:HackService:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HackService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/MapHackService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HackService:origin" defaultValue="">
          <maps>
            <mapMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/MapHackService:origin" />
          </maps>
        </aCS>
        <aCS name="HackService:uri" defaultValue="">
          <maps>
            <mapMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/MapHackService:uri" />
          </maps>
        </aCS>
        <aCS name="HackServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/MapHackServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HackService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackService/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHackService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHackService:origin" kind="Identity">
          <setting>
            <aCSMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackService/origin" />
          </setting>
        </map>
        <map name="MapHackService:uri" kind="Identity">
          <setting>
            <aCSMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackService/uri" />
          </setting>
        </map>
        <map name="MapHackServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HackService" generation="1" functional="0" release="0" software="C:\Users\uffe\Desktop\HackZurich\HackZurich.CloudService\csx\Release\roles\HackService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="tcp" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="origin" defaultValue="" />
              <aCS name="uri" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HackService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HackService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="HackServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="HackServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HackServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="14af5468-a5da-4a00-939c-234105a71608" ref="Microsoft.RedDog.Contract\ServiceContract\HackZurich.CloudServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="48754fe9-f8d2-4815-924b-5e067119a42f" ref="Microsoft.RedDog.Contract\Interface\HackService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/HackZurich.CloudService/HackZurich.CloudServiceGroup/HackService:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>