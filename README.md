# Energy Use

Energy Use is a program to analyze and predict the used energy. With energy is mainly meant the energy used by a home like electricity, gas and water. The program can show the energy used by year, month, week and day and based on the historic data it will try to predict the future use. Due to every changing rules and tariffs, this program will give you an inside in the future of your energy use and the cost.

Data that can be entered and the programs uses:

    Energy used
    Tariff of energy
    Tariffs for taxed over energy use
    Payments made to an energy provider

Based on the data, the program can:

    Show data per period
    Compare energy by period
    Show development of the tariffs over time
    Predict in how many the return on invest of solar panels is
    Simulate a settlement is with your energy provider

## Back-up and restore

Both desktop interfaces create consistent SQLite snapshots, including committed data in the write-ahead log (WAL). Backups are standalone database files with unique names. A backup is published only after integrity, schema compatibility, and foreign key checks succeed. The default backup folder is `%LOCALAPPDATA%\EnergyUse\BackUp`.

Restore validates a temporary snapshot before changing the current database. When "backup before restore" is enabled, a failed safety backup aborts restoration. SQLite restores the snapshot in a transaction; a failed copy leaves the current database unchanged. Restoring the current file onto itself is rejected. Older databases without the tariff/category join table are supported; the app creates that table on the next start.

Restore asks for confirmation, replaces all saved data, and discards unsaved changes. After a successful restore, the app closes to discard cached records. Start it again to use the restored database. Backup and restore errors are shown without closing the app. Store important backups on another drive or in a separately backed-up location.
