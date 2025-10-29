# PocketGraph Website

This is the GitHub Pages website for PocketGraph - a Neo4j mobile client for iOS.

## Setup GitHub Pages

### Automatic Deployment (Recommended)

This repository includes GitHub Actions for automatic deployment! 🚀

1. Go to your repository **Settings** on GitHub
2. Navigate to **Pages** in the left sidebar
3. Under "Source", select **GitHub Actions**
4. That's it! The site will automatically deploy on every push to main

Your website will be available at: `https://kl0070.github.io/xamarin-neo4j/`

### Manual Setup (Alternative)

If you prefer manual deployment:

1. Go to your repository settings on GitHub
2. Navigate to **Pages** in the left sidebar
3. Under "Source", select the branch (usually `main`)
4. Set the folder to `/docs`
5. Click **Save**

## GitHub Actions Workflows

This project includes two workflows:

- **Deploy GitHub Pages** (`.github/workflows/deploy-pages.yml`)
  - Automatically deploys the site when changes are pushed to `/docs`
  - Can be manually triggered from the Actions tab
  
- **Validate Website** (`.github/workflows/validate-site.yml`)
  - Validates HTML structure
  - Checks for broken links
  - Runs on pull requests and pushes

## Local Development

To preview the website locally:

1. Open `index.html` in your browser directly, or
2. Use a simple HTTP server:

```bash
# Using Python 3
cd docs
python3 -m http.server 8000

# Using Node.js (if you have http-server installed)
cd docs
npx http-server
```

Then open `http://localhost:8000` in your browser.

## Files

- `index.html` - Main landing page
- `css/style.css` - Stylesheet
- `images/` - Logo and graphics
  - `logo.png` - PocketGraph logo
  - `resoftware.png` - Re: Software logo

## Features

The website includes:

- 📱 Responsive design (mobile, tablet, desktop)
- 🎨 Beautiful gradient UI with iPhone mockups
- ⚡ Fast loading, pure HTML/CSS (no build step required)
- 🔗 Direct App Store download link
- 📊 Feature showcase with interactive cards
- 🖼️ Multiple iPhone mockups showing app screens

## Customization

To customize the website:

- **Colors**: Edit CSS variables in `css/style.css` (`:root` section)
- **Content**: Edit text directly in `index.html`
- **Images**: Replace files in `images/` folder

Enjoy!

