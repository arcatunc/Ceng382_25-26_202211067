// Enhance interactivity by adding at least three JavaScript events. For example: Row Click Event: When a row is clicked, its details are logged to the console and the row is visually highlighted. • Table Click Event: When the table is clicked (outside individual rows), an array containing all class entries is printed to the console. • Input Focus Event: When a form field gains focus, its styling is updated to indicate that it is active. • Input Blur Event: When a form field loses focus, the entered data is validated and the styling is reset if necessary. • Form Submit Event: When the form is submitted, the entered data is dynamically added to the table without reloading the page. • Mouseover Event: When the mouse hovers over a table row, its background color temporarily changes. • Mouseout Event: When the mouse leaves a table row, its background color reverts to its default state. • Double-click Event: When a row is double-clicked, additional actions like displaying detailed information or removing the row are triggered. • Keyup/Keydown Event: As keys are pressed within a form field, real-time validation provides immediate feedback.

// select 3 of these js events. Implement it according to my css style and lord of the rings theme.


// Canlı saat göstergesi
function updateClock() {
    const clockElement = document.getElementById('clock');
    const now = new Date();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    clockElement.textContent = `${hours}:${minutes}:${seconds}`;
}
setInterval(updateClock, 1000);
updateClock();

// 'H' tuşuna basıldığında web sitesindeki tüm HTML formlarının gizlendiği ve tekrar basıldığında bunların yeniden görüntülendiği bir özellik uygulayın.
// 'H' tuşuna basıldığında login div'ini gizle/göster
// h tuşuna basıldığında direkt bütün div class kapanması lazım.
let loginVisible = true;
document.addEventListener('keydown', (event) => {
    const isInputFocused = document.activeElement.tagName === 'INPUT'; // Kullanıcı adı ya da şifre alanına odaklanıldığını kontrol et
    if (!isInputFocused && event.key.toLowerCase() === 'h') {
        const loginDiv = document.querySelector('.login');
        loginVisible = !loginVisible;
        loginDiv.style.display = loginVisible ? '' : 'none';
    }
});

// Kullanıcı adı ve şifreyi saklamak için bir array
const usersArray = [];
// JavaScript kullanarak, bir kullanıcı oturum açmaya çalıştığında ve oturum açma düğmesine tıkladığında kullanıcı adı ve parolası arka planda bir dizide saklanacak şekilde oturum açma ekranını değiştirin. Ardından,
// bu dizinin tüm öğelerini konsola yazdırın
// Giriş yapma işlemi
document.getElementById('loginButton').addEventListener('click', () => {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
// kullanıcı adı ve şifreye admin yazıldığında table.html e yönlendiren kodu ekle
    if (username && password) {
        if (username === 'admin' && password === 'admin') {
            window.location.href = 'table.html';
        } else {
            usersArray.push({ username: username, password: password });
            console.log(usersArray);
            document.getElementById('username').value = '';
            document.getElementById('password').value = '';
        }
    } else {
        alert('Kullanıcı adı ve şifreyi doldurduğunuzdan emin olun.');
    }
});
