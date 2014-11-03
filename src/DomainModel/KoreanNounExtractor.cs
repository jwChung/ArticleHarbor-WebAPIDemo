namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using kr.ac.kaist.swrc.jhannanum.comm;
    using kr.ac.kaist.swrc.jhannanum.hannanum;

    public class KoreanNounExtractor
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

                    foreach (var eojeol in sentence.Eojeols)
                        foreach (var morpheme in eojeol.Morphemes)
                            yield return morpheme;
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