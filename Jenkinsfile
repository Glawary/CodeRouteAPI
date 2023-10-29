#!groovy

node {

    try {
        stage 'Checkout'
            checkout scm

            sh 'git log HEAD^..HEAD --pretty="%h %an - %s" > GIT_CHANGES'
            

        stage 'Deploy'
	    sh "chmod +x -R ${env.WORKSPACE}"
            sh './deploy.sh'


       
    }

    catch (err) {
       

        throw err
    }

}
