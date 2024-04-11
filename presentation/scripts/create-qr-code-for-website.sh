#!/bin/sh

# Invoke from this folder, otherwise change target location (`-o "..."`)
# Also adopt URL (last parameter)...

qrencode -s 6 -l H -o "../public/images/slides.png" "https://draptik.github.io/2024-4-mathemacampus-approval-testing/"

qrencode -s 6 -l H -o "../public/images/sample-code.png" "https://github.com/draptik/2024-04-mathemacampus-approval-testing"
