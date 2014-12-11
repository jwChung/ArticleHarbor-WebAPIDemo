namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using kr.ac.kaist.swrc.jhannanum.comm;
    using kr.ac.kaist.swrc.jhannanum.hannanum;

    public static class KoreanNounExtractor
    {
        private static object syncLock = new object();

        public static IEnumerable<string> Execute(string document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            lock (syncLock)
            {
                Workflow workflow = WorkflowFactory.getPredefinedWorkflow(
                    WorkflowFactory.WORKFLOW_NOUN_EXTRACTOR);
                try
                {
                    workflow.activateWorkflow(true);
                    workflow.analyze(document);
                    var sentence = workflow.getResultOfSentence(new Sentence(0, 0, false));

                    return sentence.Eojeols.SelectMany(e => e.Morphemes)
                        .Distinct(StringComparer.CurrentCultureIgnoreCase);
                }
                finally
                {
                    /* Shutdown the work flow */
                    workflow.close();
                }
            }
        }
    }
}