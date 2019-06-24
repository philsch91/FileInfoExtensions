# FileInfoExtensions

The FileInfoExtensions.dll extends the sealed System.IO.FileInfo class

#### Enums
- FileState

```csharp
FileInfo file = new FileInfo("test.txt");
FileState state = file.GetFileState();

if(state == FileState.Locked){
	//the file is locked because it is
	//1. still beeing written to
	//2. a file system handle exists from another process or thread
} else if(state == FileState.NotExistent){
	//the file does not exist anymore
	//and may has already been processed
} else if (state == FileState.Unknown){
	//unknown file state
} else {
	//the file exists and is not currently processed 
	//state == FileState.Existent
}
```
