//Javascript Code..

document.addEventListener('DOMContentLoaded', () => {

    //Dom Elements
    const loadMoreButton = document.querySelector('.load-more-button');
    const postsContainer = document.getElementById('add-more-posts');
    const checkBoxesFilter = document.querySelectorAll('input[type="checkbox"]');
    const applyFilterBtn = document.getElementById('applyFilter');
    const addCommentBtn = document.querySelector('.add-comment');
    const commentsForm = document.getElementById('comments-form');
    let postsToSkip = 0;

    //const storedMenuItem = localStorage.getItem('newMenuItem');

    //const menuItem = JSON.parse(storedMenuItem)
    //console.log(menuItem);

    //const menuListItem = document.createElement('li');
    //menuListItem.innerHTML = menuItem;
    //document.getElementById('main-menu').appendChild(menuListItem);

    //AddComment
    if (commentsForm) { 
    commentsForm.addEventListener('submit', async (e) => {

        e.preventDefault();
        const postID = document.getElementById('comment-post-id').value;
        const email = document.getElementById('comment-email').value;
        const name = document.getElementById('comment-name').value;
        const commentBody = document.getElementById('comment-body').value;

        try {

            console.log('api controller called.');
            const response = await fetch(`/api/AddComment?Email=${email}&Name=${name}&CommentBody=${commentBody}&PostID=${postID}`, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {

                let commentContainer = document.querySelector('.add-comment-container');
                commentContainer.innerHTML = '';
                commentContainer.textContent = "Comment submitted and is pending approval";

                 
            } else {

                console.log("Comment was not added to database!");
            }


        } catch (err) {

            console.error("Error Adding Comment", err);
        }

    })
}

    //Load More Ajax Functionality (Load More Blog Posts)
    if (loadMoreButton) {
        loadMoreButton.addEventListener('click', async () => {

            let selectedCategories = [];
            checkBoxesFilter.forEach(box => box.checked ? selectedCategories.push(box.value) : false);
            let categories = selectedCategories.join(',');

            try {
                postsToSkip = postsContainer.childElementCount;
                const response = await fetch(`/Home/LoadMorePosts?postsToSkip=${postsToSkip}&filterBy=${categories}`, {
                    method: 'GET'
                });

                if (!response.ok) {

                    console.error("error");
                }

                const html = await response.text();
                postsContainer.insertAdjacentHTML('beforeend', html);

            } catch (err) {

                console.error(`error occured fetching posts data: ${err}`);
            }
        })
    }


    //CheckBox Filter Logic
    if (applyFilterBtn) {
        applyFilterBtn.addEventListener('click', async () => {

            let selectedCategories = [];

            checkBoxesFilter.forEach(box => box.checked ? selectedCategories.push(box.value) : false);
            let categories = selectedCategories.join(',');

            try {

                const response = await fetch(`/AllRecipes?categories=${categories}`);

                if (response.ok) {

                    console.log("fetching filtered blog posts...");
                }

                const html = await response.text();

                //clear container and add filtered posts.
                postsContainer.innerHTML = '';
                postsContainer.insertAdjacentHTML('beforeend', html);

            } catch (err) {

                console.error(`error fetching data: ${err}`);
            }
        })
    }


    //END
})