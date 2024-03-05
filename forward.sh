git checkout .
git clean -fd
git checkout $(git log --reverse --pretty=%H | head -n 1)
