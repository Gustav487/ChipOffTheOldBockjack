```mermaid
classDiagram
    NNode <|-- NInfoOnHover
    
    class NInfoOnHover {
        [Export] -Control _hover
        [Export] -CanvasItem _info
    }
```