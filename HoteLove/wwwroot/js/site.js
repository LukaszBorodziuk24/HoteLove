document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('[id^="commentForm-"]').forEach(form => {
        form.addEventListener('submit', async function (e) {
            e.preventDefault();
            
            const formData = {
                hotelId: this.querySelector('input[name="hotelId"]').value,
                content: this.querySelector('textarea[name="content"]').value
            };
            
            const response = await fetch('/Home/CreateComment', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                const commentContent = this.closest('.comment-section').querySelector('.comment-content');
                const newComment = `
                    <div class="comment">
                        <small class="text-muted" style="font-size: 12px;">
                            ${new Date().toLocaleString('pl-PL')}
                        </small>
                        <div class="comment-text">
                            ${formData.content}
                        </div>
                    </div>
                `;
                
                commentContent.insertAdjacentHTML('afterbegin', newComment);
                this.querySelector('textarea').value = '';
            }
        });
    });
});




document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.rating-form').forEach(form => {
        form.addEventListener('submit', async function (e) {
            e.preventDefault();

            const hotelId = this.querySelector('input[name="hotelId"]').value;
            const value = e.submitter.value;

            const formData = {
                HotelId: hotelId,
                Value: parseInt(value, 10)
            };

            try {
                const response = await fetch('/Home/CreateRating', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(formData)
                });

                if (response.ok) {
                    const result = await response.json();

                    const ratingCount = this.closest('.rating-buttons-container').querySelector('.rating-count');

                    ratingCount.textContent = result.newAverageRating.toFixed(1).replace('.', ',');
                } else {
                    const error = await response.json();
                    alert(error.error || "Wystąpił błąd podczas przesyłania oceny.");
                }
            } catch (err) {
                console.error("Błąd sieci:", err);
                alert("Wystąpił błąd sieci. Spróbuj ponownie później.");
            }
        });
    });
});


