# Structural Patterns Extractor

Specify source code directory path as cli argument. Result report markdown will be created in current directory (where executed)


### Parsing

Only C# code can be analysed at the moment.
New parsers can easily be created by implementing IParser interface (Parsing directory).

### Patterns Extraction

Supported patterns:  
* Bridge  
* Composite  
* Facade  
* Flyweight  
* Proxy  
* Decorator  

New extractors can be created by implementing IPatternExtractor interface (Analysis directory)

### Examples

Examples folder contains open source repositories analysis reports