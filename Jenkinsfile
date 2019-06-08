pipeline {
  agent any
  stages {
    stage('Restore PACKAGES') {
      steps {
        bat "dotnet restore --configfile NuGet.Config"
       }
    }
  }
}
