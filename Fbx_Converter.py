import aspose.threed as a3d
from aspose.threed import Scene
import PySimpleGUI as sg

# Create a layout for the GUI
layout = [
    [sg.Text("Select an STL file")],
    [sg.Input(key="-INPUT-"), sg.FileBrowse(file_types=[("STL files", "*.stl")])],
    [sg.Text("Save as FBX file")],
    [sg.Input(key="-OUTPUT-"), sg.FileSaveAs(file_types=[("FBX files", "*.fbx")])],
    [sg.Button("Convert"), sg.Button("Cancel")]
]

# Create a window with the layout
window = sg.Window("File Converter", layout)

# Loop until the user clicks a button or closes the window
while True:
    event, values = window.read()
    if event == "Convert":
        # Get the input and output file paths
        input_file = values["-INPUT-"]
        output_file = values["-OUTPUT-"]

        # Load STL file
        scene = Scene.from_file(input_file)

        # Save STL as FBX
        scene.save(output_file)

        # Show a message that the conversion is done
        sg.popup("Conversion done!")
    elif event == "Cancel" or event == sg.WIN_CLOSED:
        # Exit the loop
        break

# Close the window
window.close()