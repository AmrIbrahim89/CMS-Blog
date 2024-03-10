//JavaScript code//

document.addEventListener('DOMContentLoaded', () => {

    //Dom Elements
    const blogPostsContainer = document.querySelectorAll('.blog-posts-container');
    const mainMenu = document.getElementById('main-menu');
    const addPageToMenuBtn = document.querySelectorAll('.add-page-to-menu');
    const darkModeIcon = document.querySelector('.fa-moon');
    const showEmail = document.querySelector('.reveal-email');
    const profileEmailInput = document.getElementById('profile-email-address');
    const approveCommentBtn = document.querySelectorAll('.approve');
    const dismissCommentBtn = document.querySelectorAll('.dismiss');
    const card = document.querySelector(".card");

    //Approve Comments
    if (approveCommentBtn) {

        approveCommentBtn.forEach(btn => btn.addEventListener('click', async () => {

            const commentID = btn.getAttribute("data-comment-id");

            try {
                const response = await fetch(`/api/CommentApproval?commentID=${commentID}`, {
                    method: 'patch',
                })

                if (response.ok) {

                    const commentContainer = btn.closest("#comment-data");
                    commentContainer.remove();
                }

            } catch (err) {

                console.error("error approving comment", err);
            }
        }))
    }


    //Dismiss Comments
    if (dismissCommentBtn) {

        dismissCommentBtn.forEach(btn => btn.addEventListener('click', async () => {

            const commentID = btn.getAttribute("data-comment-id");

            try {
                const response = await fetch(`/api/DismissComment?commentID=${commentID}`, {
                    method: 'delete',
                })

                if (response.ok) {

                    const commentContainer = btn.closest("#comment-data");
                    commentContainer.classList.add('fade-out');

                    commentContainer.addEventListener('transitionend', () => {
                        commentContainer.remove();
                    })                  
                }

            } catch (err) {

                console.error("error approving comment", err);
            }
        }))
    }


    //Add BlogPost to featured posts upon click
    if (blogPostsContainer) {
        blogPostsContainer.forEach(post => post.addEventListener('click', async (event) => {
            const postIdInput = event.target.closest('.blog-post').querySelector('.post-id');
            if (postIdInput) {
                const postId = postIdInput.value;
                console.log(`Clicked on post with ID ${postId}`);

                try {
                    const response = await fetch(`/api/blogposts/${postId}/feature`, {
                        method: 'PATCH',
                    });

                    if (response.ok) {

                        if (event.target.classList.contains('marked-featured')) {

                            event.target.classList.remove('marked-featured');

                        } else {

                            event.target.classList.add('marked-featured');
                        }
                    } else {

                        console.error('Error updating feature status.');
                    }
                } catch (error) {

                    console.error('An error occurred:', error);
                }

            }
        }))
    }


    //Reveal Email
    if (showEmail) {
        showEmail.addEventListener('click', async () => {

            let email = profileEmailInput.dataset.userEmail;

            profileEmailInput.placeholder = email;
        })
    }


    //AddPageToMainMenu
    //if (addPageToMenuBtn) {
    //    addPageToMenuBtn.forEach(btn => btn.addEventListener('click', async () => {

    //        try {

    //            const pageID = document.querySelector('.page-id');
    //            const response = await fetch(`/api/AddPageToMenu?pageID=${pageID.value}`, { method: 'GET' });

    //            if (!response.ok) {

    //                console.log('Page was not added to main menu');
    //            }

    //            const page = await response.text();
    //            //const page = await response.json();
    //            console.log(page);

    //            localStorage.setItem('newMenuItem', JSON.stringify(page));

    //        } catch (err) {

    //            console.error(`Error occured while adding page to main menu ${err}`);
    //        }
    //    }))
    //}


    function darkMode() {
        document.body.classList.toggle('dark-mode');
        document.querySelector('.current-user-name').classList.toggle('text-white');
        document.querySelector('.current-user-status').classList.toggle('text-white');
        document.querySelectorAll('.stats-tile').forEach(el => el.classList.toggle('dark-mode-black'));
        document.querySelectorAll('.sale-details').forEach(el => el.classList.toggle('text-white'));
        document.querySelectorAll('.card').forEach(card => card.classList.toggle('dark-mode-black'));
        document.querySelector('.card-title').classList.toggle('text-white');
        document.querySelectorAll('.comment-body').forEach(c => c.classList.toggle('text-white'));
        document.querySelectorAll('.comment-meta').forEach(c => c.classList.toggle('text-white'));
        document.querySelector('.sidebar-wrapper').classList.toggle('dark-mode');
        document.querySelectorAll('.accordion-body').forEach(m => m.classList.toggle('dark-mode-black'));
        document.querySelectorAll('.accordion-button').forEach(m => m.classList.toggle('dark-mode'));
        document.querySelectorAll('.accordion-button').forEach(m => m.classList.toggle('text-white'));
        document.querySelector('.menu-text').classList.toggle('text-white');
        document.querySelectorAll('.menu-item').forEach(el => el.classList.toggle('text-white'));
        document.querySelector('.copyright').classList.toggle('text-white');
    }


    //const isDarkModeEnabled = localStorage.getItem("darkModeEnabled");

    //// Apply dark mode styles if enabled
    //if (isDarkModeEnabled) {
    //    darkMode();
    //}

    //Enable Dark Mode
    darkModeIcon.addEventListener('click', () => {

        darkMode();
        localStorage.setItem("darkModeEnabled", document.body.classList.contains('dark-mode'));
    })

    //Add elements to menu
    if (addPageToMenuBtn) {

        addPageToMenuBtn.addEventListener('click', async () => {

            try {

                const response = await fetch(`/api/AddMenuItem`, { method: "PATCH" });

                if (!response.ok) {

                    console.log("Menu item was not added.");
                }

                console.log("Menu item added Successfully!");


            } catch (err) {

                console.error(err);
            }



        });
    }
     
    //END..
});

