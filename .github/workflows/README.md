# GitHub Actions Workflows

This directory contains automated workflows for the PocketGraph project.

## Workflows

### 🚀 Deploy GitHub Pages (`deploy-pages.yml`)

**Purpose**: Automatically deploys the static website to GitHub Pages

**Triggers**:
- Push to `main` branch (when files in `/docs` change)
- Manual trigger via Actions tab

**What it does**:
1. Checks out the repository
2. Configures GitHub Pages
3. Uploads the `/docs` folder as an artifact
4. Deploys to GitHub Pages

**Permissions Required**:
- `contents: read`
- `pages: write`
- `id-token: write`

**Environment**: Deploys to `github-pages` environment

---

### ✅ Validate Website (`validate-site.yml`)

**Purpose**: Validates HTML and checks for broken links

**Triggers**:
- Pull requests that modify files in `/docs`
- Push to `main` branch (when files in `/docs` change)

**What it does**:
1. Validates HTML5 compliance
2. Checks all links for broken URLs
3. Reports errors if validation fails

---

## Setup Instructions

### First Time Setup

1. **Enable GitHub Pages with Actions**:
   - Go to repository **Settings** → **Pages**
   - Under "Source", select **GitHub Actions**
   - Save

2. **Verify Workflows**:
   - Go to the **Actions** tab
   - You should see the workflows listed
   - They will run automatically on the next push to `main`

### Manual Deployment

You can manually trigger a deployment:

1. Go to **Actions** tab
2. Select "Deploy GitHub Pages" workflow
3. Click "Run workflow"
4. Select branch (usually `main`)
5. Click "Run workflow"

## Monitoring

- **Status Badges**: The main README includes a status badge showing deployment status
- **Action Logs**: Click on any workflow run in the Actions tab to see detailed logs
- **Deployment URL**: After successful deployment, the site is available at `https://kl0070.github.io/xamarin-neo4j/`

## Troubleshooting

**Deployment fails with permission error**:
- Ensure GitHub Pages is enabled in repository settings
- Check that Actions have the required permissions

**Validation fails**:
- Check the validation logs for specific HTML errors
- Fix broken links identified by the link checker

**Site not updating**:
- Check the Actions tab for failed deployments
- Ensure changes were pushed to the `main` branch
- Clear browser cache and wait a few minutes for GitHub's CDN to update

