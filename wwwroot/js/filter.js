


document.addEventListener('DOMContentLoaded', function () {
    let selectAllChecbox = document.getElementById('selectAll');

    const searchInput = document.getElementById('search-input');
    const statusFilter = document.getElementById('status-filter');
    //const typeFilter = document.getElementById('type-filter');
    //const resetFiltersBtn = document.getElementById('reset-filters');
    const complaintTable = document.getElementById('complaintTables');
    const tableRows = complaintTable.getElementsByTagName('tbody')[0].getElementsByTagName('tr');

    // Function to filter and search the table
    function updateTable() {
        const searchTerm = searchInput.value.toLowerCase();
        const statusValue = statusFilter.value.trim().toLowerCase();
        
        console.log(`Statusvalue is:  ${statusValue}`);
        //const typeValue = typeFilter.value;

        for (let row of tableRows) {
            const subject = row.querySelector('[data-column="subject"]').textContent.toLowerCase();
            const description = row.querySelector('[data-column="description"]').textContent.toLowerCase();
            const status = row.querySelector('[data-column="status"]').textContent.trim().toLowerCase();
            const type = row.querySelector('[data-column="type"]').textContent.toLowerCase();
            const date = row.querySelector('[data-column="date"]').textContent.toLowerCase();
            const transformDirection = row.querySelector('[data-column="transformDirection"]').textContent.toLowerCase();

            console.log(`value is:  ${status} = ${statusValue}`);
            

            // Check if row matches search term (in subject or description)
            const matchesSearch = searchTerm === '' ||
                subject.includes(searchTerm) ||
                description.includes(searchTerm) ||
                status.includes(searchTerm) ||
                type.includes(searchTerm) ||
                date.includes(searchTerm) ||
                    transformDirection.includes(searchTerm);

            // Check if row matches status filter
            const matchesStatus = statusValue === '' || status.toLowerCase() === statusValue.toLowerCase();

            // Check if row matches type filter
            //const matchesType = typeValue === '' || type === typeValue;

            // Show/hide row based on filters
            if (matchesSearch && matchesStatus) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        }
    }

    // Event listeners
    searchInput.addEventListener('input', updateTable);
    statusFilter.addEventListener('change', updateTable);
    //typeFilter.addEventListener('change', updateTable);

    //resetFiltersBtn.addEventListener('click', function () {
    //    searchInput.value = '';
    //    statusFilter.value = '';
    //    typeFilter.value = '';
    //    updateTable();
    //});

    let childCheck = document.getElementsByName("selectedIds");
    let counter = 0;
    selectAllChecbox.addEventListener('change', () => {

        counter = counter + 1;
        console.log(counter)
        console.log(`Checkbox Value is : ${selectAllChecbox.value}`);
        if (counter % 2 == 0) {
            $('input[name="selectedIds"]').prop('checked', false);

        } else {
            $('input[name="selectedIds"]').prop('checked', true);
}
        
        console.log(`Child Checkbox Value is : ${childCheck.value}`);
    })

  

   
});

// Select/deselect all checkboxes

