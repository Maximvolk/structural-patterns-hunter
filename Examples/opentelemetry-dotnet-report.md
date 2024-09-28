
---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* OtlpExporterBuilderOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/Builder/OtlpExporterBuilderOptions.cs, line 13) -> Facade  
* SdkLimitOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/Implementation/SdkLimitOptions.cs, line 8) -> Subsystem  
* ExperimentalOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/Implementation/ExperimentalOptions.cs, line 8) -> Subsystem  
* OtlpExporterOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/OtlpExporterOptions.cs, line 25) -> Subsystem  

---
>                        Decorator Design Pattern
>
>Intent: Lets you attach new behaviors to objects by placing these objects
>inside special wrapper objects that contain the behaviors.    

* MetricTestsBase (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricTestsBase.cs, line 17) -> Base component  
* MetricExemplarTests (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricExemplarTests.cs, line 15) -> Concrete component  
* MetricViewTests (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricViewTests.cs, line 11) -> Concrete component  
* MetricApiTestsBase (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricApiTestsBase.cs, line 17) -> Base decorator  
* MetricApiTestWithReclaimAttribute (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricApiTestsBase.cs, line 1823) -> Concrete decorator  
* MetricApiTestWithOverflowAttribute (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricApiTestsBase.cs, line 1815) -> Concrete decorator  
* MetricApiTest (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricApiTestsBase.cs, line 1807) -> Concrete decorator  
* MetricApiTestWithBothOverflowAndReclaimAttributes (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Metrics/MetricApiTestsBase.cs, line 1831) -> Concrete decorator  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* SpanShim (../opentelemetry-dotnet/src/OpenTelemetry.Shims.OpenTracing/SpanShim.cs, line 10) -> Facade  
* SpanContextShim (../opentelemetry-dotnet/src/OpenTelemetry.Shims.OpenTracing/SpanContextShim.cs, line 8) -> Subsystem  
* TelemetrySpan (../opentelemetry-dotnet/src/OpenTelemetry.Api/Trace/TelemetrySpan.cs, line 14) -> Subsystem  

---
>                        Composite Design Pattern
>
>Intent: Lets you compose objects into tree structures and then work with
>these structures as if they were individual objects.    

* Sampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/Sampler.cs, line 11) -> Base component  
* StratifiedSampler (../opentelemetry-dotnet/docs/trace/stratified-sampling-example/StratifiedSampler.cs, line 8) -> Concrete composite  
* ParentBasedElseAlwaysRecordSampler (../opentelemetry-dotnet/docs/trace/tail-based-sampling-span-level/ParentBasedElseAlwaysRecordSampler.cs, line 18) -> Concrete leaf  
* AlwaysOnSampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/AlwaysOnSampler.cs, line 9) -> Concrete leaf  
* LinksBasedSampler (../opentelemetry-dotnet/docs/trace/links-based-sampler/LinksBasedSampler.cs, line 13) -> Concrete leaf  
* TraceIdRatioBasedSampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/TraceIdRatioBasedSampler.cs, line 12) -> Concrete leaf  
* ParentBasedSampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/ParentBasedSampler.cs, line 18) -> Concrete leaf  
* AlwaysOffSampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/AlwaysOffSampler.cs, line 9) -> Concrete leaf  
* ThrowingSampler (../opentelemetry-dotnet/test/OpenTelemetry.Tests/Trace/SamplersTest.cs, line 261) -> Concrete leaf  
* LinksAndParentBasedSampler (../opentelemetry-dotnet/docs/trace/links-based-sampler/LinksAndParentBasedSampler.cs, line 16) -> Concrete leaf  

---
>                        Composite Design Pattern
>
>Intent: Lets you compose objects into tree structures and then work with
>these structures as if they were individual objects.    

* TextMapPropagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/TextMapPropagator.cs, line 11) -> Base component  
* CompositeTextMapPropagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/CompositeTextMapPropagator.cs, line 12) -> Concrete composite  
* TestPropagator (../opentelemetry-dotnet/test/OpenTelemetry.Api.Tests/Context/Propagation/TestPropagator.cs, line 8) -> Concrete leaf  
* NoopTextMapPropagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/NoopTextMapPropagator.cs, line 6) -> Concrete leaf  
* JaegerPropagator (../opentelemetry-dotnet/src/OpenTelemetry.Extensions.Propagators/JaegerPropagator.cs, line 13) -> Concrete leaf  
* BaggagePropagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/BaggagePropagator.cs, line 16) -> Concrete leaf  
* TraceContextPropagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/TraceContextPropagator.cs, line 14) -> Concrete leaf  
* B3Propagator (../opentelemetry-dotnet/src/OpenTelemetry.Extensions.Propagators/B3Propagator.cs, line 15) -> Concrete leaf  
* B3Propagator (../opentelemetry-dotnet/src/OpenTelemetry.Api/Context/Propagation/B3Propagator.cs, line 15) -> Concrete leaf  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* Base2ExponentialBucketHistogram (../opentelemetry-dotnet/src/OpenTelemetry/Metrics/MetricPoint/Base2ExponentialBucketHistogram.cs, line 15) -> Facade  
* ExponentialHistogramData (../opentelemetry-dotnet/src/OpenTelemetry/Metrics/MetricPoint/ExponentialHistogramData.cs, line 9) -> Subsystem  
* CircularBufferBuckets (../opentelemetry-dotnet/src/OpenTelemetry/Metrics/CircularBufferBuckets.cs, line 13) -> Subsystem  

---
>                        Decorator Design Pattern
>
>Intent: Lets you attach new behaviors to objects by placing these objects
>inside special wrapper objects that contain the behaviors.    

* BaseProvider (../opentelemetry-dotnet/src/OpenTelemetry.Api/BaseProvider.cs, line 9) -> Base component  
* OpenTelemetryLoggerProvider (../opentelemetry-dotnet/src/OpenTelemetry/Logs/ILogger/OpenTelemetryLoggerProvider.cs, line 17) -> Concrete component  
* TracerProvider (../opentelemetry-dotnet/src/OpenTelemetry.Api/Trace/TracerProvider.cs, line 14) -> Base decorator  
* TestTracerProvider (../opentelemetry-dotnet/test/OpenTelemetry.Api.Tests/Trace/TracerTest.cs, line 422) -> Concrete decorator  
* TracerProviderSdk (../opentelemetry-dotnet/src/OpenTelemetry/Trace/TracerProviderSdk.cs, line 15) -> Concrete decorator  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* SpanBuilderShim (../opentelemetry-dotnet/src/OpenTelemetry.Shims.OpenTracing/SpanBuilderShim.cs, line 15) -> Facade  
* Tracer (../opentelemetry-dotnet/src/OpenTelemetry.Api/Trace/Tracer.cs, line 14) -> Subsystem  
* SpanAttributes (../opentelemetry-dotnet/src/OpenTelemetry.Api/Trace/SpanAttributes.cs, line 13) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* OpenTelemetryLogger (../opentelemetry-dotnet/src/OpenTelemetry/Logs/ILogger/OpenTelemetryLogger.cs, line 12) -> Facade  
* LoggerProviderSdk (../opentelemetry-dotnet/src/OpenTelemetry/Logs/LoggerProviderSdk.cs, line 18) -> Subsystem  
* OpenTelemetryLoggerOptions (../opentelemetry-dotnet/src/OpenTelemetry/Logs/ILogger/OpenTelemetryLoggerOptions.cs, line 13) -> Subsystem  
* InstrumentationScopeLogger (../opentelemetry-dotnet/src/OpenTelemetry/Internal/InstrumentationScopeLogger.cs, line 9) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* LinksAndParentBasedSampler (../opentelemetry-dotnet/docs/trace/links-based-sampler/LinksAndParentBasedSampler.cs, line 16) -> Facade  
* ParentBasedSampler (../opentelemetry-dotnet/src/OpenTelemetry/Trace/Sampler/ParentBasedSampler.cs, line 18) -> Subsystem  
* LinksBasedSampler (../opentelemetry-dotnet/docs/trace/links-based-sampler/LinksBasedSampler.cs, line 13) -> Subsystem  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* IntegrationTests (../opentelemetry-dotnet/test/OpenTelemetry.Exporter.OpenTelemetryProtocol.Tests/IntegrationTest/IntegrationTests.cs, line 18) -> Facade  
* SdkLimitOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/Implementation/SdkLimitOptions.cs, line 8) -> Subsystem  
* ExperimentalOptions (../opentelemetry-dotnet/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/Implementation/ExperimentalOptions.cs, line 8) -> Subsystem  
