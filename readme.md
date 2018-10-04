# Improving Data Transfer between Device and Database

The project uses `C#` to implement a better system for data transfer between device and database for MES Medical, which has a device needed to be calibrated every couple months.

In practice, the calibration process would involve multiple capillaries and devices that aretested in parallel. As a result, it is difficult to distinguish the capillaries and there might beconfusion about the capillaries. For example, it would be hard to tell which capillary has beentested in a device. Mes Medical has decided to enhance its process by automating several of themanual steps. One of the most crucial step that needs to be improved is the identification ofcapillaries.

My task was to improve the calibration process by setting up the basis for automaticcalibration. First of all, I needed to develop a solution that allows tracking the capillaries insertedinto each device. The solution should include an automatic capillary identification mechanism.My approach is to create a barcode marked on each capillary and there will be a barcode readerthat is attached to every device during the calibration. The capillary should be scan in eachdevice before using it. In this way, it is able to keep track of the capillaries and avoid confusion.