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

## Reviewing imports

Both interfaces prepare a separate import preview before saving meter readings. It shows all affected meters, source row numbers, existing and incoming readings, and counts of new, changed, unchanged, and conflicting days. Identical source rows are grouped visibly; different readings on the same day require an explicit source-row choice or Skip. Missing or overlapping meter periods and existing duplicate database days cannot be silently resolved by the import. A reading is unique per address, energy type (electricity, water, etc.), and calendar day, regardless of physical meter. Existing duplicate days for that address and energy type must be reviewed separately before importing. A reading on a different physical meter for an existing day is a conflict; the import never silently inserts another reading or changes the meter assignment.

Selecting a meter only loads its stored readings; it no longer merges the last CSV automatically. In WPF, stored readings and imported readings appear together in one overview, using the original numeric columns, column order, widths and toolbar. Row colours indicate new, changed and conflicting readings. Conflict details expand below the row; select any imported row to review stored values, source rows and import choices. Stored-only readings remain visible and are not added to the import changes. Resolve choices directly in that overview, then choose Save. In WinForms, Import opens the review dialog; Save opens it again with the save action enabled. Both interfaces ask for confirmation. A changed file path or meter selection invalidates the preview. Saving checks that database readings and meter periods still match the preview, writes all choices in one transaction, and recalculates consumption differences for affected meters, including neighbours outside the CSV range. Reimporting unchanged readings does not insert duplicates.
