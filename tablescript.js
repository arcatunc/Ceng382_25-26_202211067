
// Implement functionality so that when data is entered and submitted via the form, the class table updates dynamically without reloading the page.
document.addEventListener('DOMContentLoaded', () => {
    const classForm = document.getElementById('classForm');
    const classTable = document.getElementById('classTable').getElementsByTagName('tbody')[0];

    // Form submit event (existing code)
    classForm.addEventListener('submit', (e) => {
        e.preventDefault();

        const className = document.getElementById('className').value;
        const numPeople = document.getElementById('numPeople').value;
        const classDescription = document.getElementById('classDescription').value;

        const newRow = classTable.insertRow();
        
        const cell1 = newRow.insertCell(0);
        const cell2 = newRow.insertCell(1);
        const cell3 = newRow.insertCell(2);

        cell1.textContent = className;
        cell2.textContent = numPeople;
        cell3.textContent = classDescription;

        // Add the Ring's power class to new rows
        newRow.classList.add('table-row');
        
        // Add event listeners to new row
        addRowEventListeners(newRow);

        classForm.reset();
    });

    // Function to add event listeners to rows
    function addRowEventListeners(row) {
        // Click event - Highlight row with golden glow
        row.addEventListener('click', function() {
            const rowData = {
                className: this.cells[0].textContent,
                numPeople: this.cells[1].textContent,
                description: this.cells[2].textContent
            };
            console.log('The Ring bearer has selected:', rowData);
            
            // Remove highlight from other rows
            document.querySelectorAll('.row-selected').forEach(r => {
                r.classList.remove('row-selected');
            });
            
            // Add highlight to clicked row
            this.classList.add('row-selected');
        });

        // Mouseover/Mouseout events - Ring's power effect
        row.addEventListener('mouseover', function() {
            this.style.backgroundColor = 'rgba(224, 137, 31, 0.2)';
            this.style.boxShadow = '0 0 15px rgba(255, 190, 80, 0.5)';
            this.style.transition = 'all 0.3s ease';
        });

        row.addEventListener('mouseout', function() {
            if (!this.classList.contains('row-selected')) {
                this.style.backgroundColor = '';
                this.style.boxShadow = '';
            }
        });

        // Double-click event - Remove row with mystical fade
        row.addEventListener('dblclick', function() {
            console.log('The Ring of Power banishes this entry to the shadow realm!');
            this.style.animation = 'fadeOut 0.5s ease-out forwards';
            setTimeout(() => {
                this.remove();
            }, 500);
        });
    }

    // Add event listeners to existing rows
    document.querySelectorAll('.table-row').forEach(row => {
        addRowEventListeners(row);
    });
});

// Add these CSS animations to your tablestyle.css
const style = document.createElement('style');
style.textContent = `
    @keyframes fadeOut {
        to {
            opacity: 0;
            transform: scale(0.8) translateY(-20px);
        }
    }

    .row-selected {
        background-color: rgba(224, 137, 31, 0.3) !important;
        box-shadow: 0 0 20px rgba(255, 190, 80, 0.7) !important;
    }

    .table-row {
        transition: all 0.3s ease;
    }
`;
document.head.appendChild(style);