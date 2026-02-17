```mermaid
classDiagram
    
    IClosable <-- NCloseButton : has
    NButton <|-- NCloseButton

    class IClosable{
        +Close()
    }

    class NCloseButton{
        [Export] -Node _closes
    }
```